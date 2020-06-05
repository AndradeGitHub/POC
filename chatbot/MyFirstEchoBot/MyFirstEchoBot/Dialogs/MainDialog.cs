using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

//using Microsoft.Azure.CognitiveServices.Knowledge.QnAMaker;
//using Microsoft.Azure.CognitiveServices.Knowledge.QnAMaker.Models;
//using Microsoft.Recognizers.Text.DataTypes.TimexExpression;

using MyFirstEchoBot.CognitiveServices;
using MyFirstEchoBot.Models;
using MyFirstEchoBot.Cards;
using System.Linq;

namespace MyFirstEchoBot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        private readonly LuisService _luisRecognizer;
        private readonly QnAMakerServices _qnAMakerService;
        protected readonly ILogger Logger;
        private ExampleCards exampleCards; 

        public MainDialog(LuisService luisRecognizer, QnAMakerServices qnAMakerService, ILogger<MainDialog> logger)
            : base(nameof(MainDialog))
        {
            _luisRecognizer = luisRecognizer;
            //_qnAMakerService = qnAMakerService;
            _qnAMakerService = new QnAMakerServices();
            Logger = logger;
            exampleCards = new ExampleCards();

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            //AddDialog(bookingDialog);
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                IntroStepAsync,
                ActStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }
        
        private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {           
            if (!_luisRecognizer.IsConfigured)
            {
                await stepContext.Context.SendActivityAsync(
                    MessageFactory.Text("NOTE: LUIS is not configured. To enable all capabilities, add 'LuisAppId', 'LuisAPIKey' and 'LuisAPIHostName' to the appsettings.json file.", inputHint: InputHints.IgnoringInput), cancellationToken);

                return await stepContext.NextAsync(null, cancellationToken);
            }

            //if (!_qnAMakerService.IsConfigured)
            //{
            //    await stepContext.Context.SendActivityAsync(
            //        MessageFactory.Text("NOTE: QnaMaker is not configured. To enable all capabilities, add 'QnaSubscriptionKey' and 'QnaKnowledgebaseId' to the appsettings.json file.", inputHint: InputHints.IgnoringInput), cancellationToken);

            //    return await stepContext.NextAsync(null, cancellationToken);
            //}

            //var activity = stepContext.Context.Activity;
            //var messageText = string.Empty;

            //if (activity.MembersAdded.Any(o => o.Id == activity.Recipient.Id))
            //{
            //    // Use the text provided in FinalStepAsync or the default if it is the first time.
            //    messageText = stepContext.Options?.ToString() ?? "Olá, eu sou o seu Robô Assistente! Em que posso ajudá-lo?";
            //}
            //else
            //    messageText = stepContext.Options?.ToString() ?? "Em que posso ajudá-lo?";

            var messageText = stepContext.Options?.ToString() ?? "Olá, eu sou o seu Robô Assistente! Em que posso ajudá-lo?";
            var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }

        private async Task<DialogTurnResult> ActStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (!_luisRecognizer.IsConfigured)
            {
                await stepContext.Context.SendActivityAsync(
                    MessageFactory.Text("NOTE: LUIS is not configured. To enable all capabilities, add 'LuisAppId', 'LuisAPIKey' and 'LuisAPIHostName' to the appsettings.json file.", inputHint: InputHints.IgnoringInput), cancellationToken);

                return await stepContext.BeginDialogAsync(null, cancellationToken);
            }

            //if (!_qnAMakerService.IsConfigured)
            //{
            //    await stepContext.Context.SendActivityAsync(
            //        MessageFactory.Text("NOTE: QnaMaker is not configured. To enable all capabilities, add 'QnaSubscriptionKey' and 'QnaKnowledgebaseId' to the appsettings.json file.", inputHint: InputHints.IgnoringInput), cancellationToken);

            //    return await stepContext.NextAsync(null, cancellationToken);
            //}

            //var textTypeUser = HandleSystemMessage(turnContext.Activity.Type);

            var textUser = stepContext.Result.ToString();

            var luisResult = await _luisRecognizer.RecognizeAsync<LuisModel>(stepContext.Context, cancellationToken);
            switch (luisResult.TopIntent().intent)
            {
                case LuisModel.Intent.QnAMakerMicrosoft:
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text("INTENÇÃO: QnAMakerMicrosoft"));

                    string ret = _qnAMakerService.GetAnswer(textUser);
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text(ret));
                    break;                        
                case LuisModel.Intent.Cotacao:
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text("INTENÇÃO: Cotação"));

                    var entidadeMoeda = ((Newtonsoft.Json.Linq.JValue)((Newtonsoft.Json.Linq.JContainer)luisResult.Entities.Moeda).First).Value;

                    var moedas = luisResult.Entities.Moeda;

                    break;
                case LuisModel.Intent.Cumprimento:
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text("INTENÇÃO: Cumprimento"));
                    break;
                case LuisModel.Intent.Card:
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text("INTENÇÃO: Card"));

                    string cardType = string.Empty;
                    if ((JContainer)luisResult.Entities.CardType != null)
                        if (((JContainer)luisResult.Entities.CardType).Count.Equals(0))
                            cardType = ((JValue)((JContainer)luisResult.Entities.CardType).First).Value.ToString();
                        else if (((JContainer)luisResult.Entities.CardType).Count.Equals(1))
                            cardType = ((JValue)((JContainer)luisResult.Entities.CardType).First.First).Value.ToString();

                    await CardTypeUser(cardType, stepContext, cancellationToken);

                    break;
                case LuisModel.Intent.Sobre:
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text("INTENÇÃO: Sobre"));

                    break;
                case LuisModel.Intent.None:
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text("INTENÇÃO: None"));
                    break;
                default:
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text("LUIS Não entendeu a intenção"));
                    break;
            }            
                                        
            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Restart the main dialog with a different message the second time around
            //var promptMessage = "What else can I do for you?";
            return await stepContext.ReplaceDialogAsync(InitialDialogId, "", cancellationToken);
        }

        private string HandleSystemMessage(string type)
        {
            var text = string.Empty;

            switch (type)
            {
                case ActivityTypes.DeleteUserData:

                    break;
                case ActivityTypes.ConversationUpdate:

                    break;
                case ActivityTypes.ContactRelationUpdate:

                    break;
                case ActivityTypes.Typing:
                    text = "Está demorando para responder, ainda está ae?";
                    break;
            }

            return text;
        }

        private Task<ResourceResponse> CardTypeUser(string cardType, WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            dynamic ret = new object();

            switch (cardType)
            {
                case "HeroCard":
                    ret = stepContext.Context.SendActivityAsync(MessageFactory.Attachment(exampleCards.CreateHeroCard().ToAttachment(), "Este é o HeroCard!!!"), cancellationToken);
                    break;
                case "AudioCard":
                    ret = stepContext.Context.SendActivityAsync(MessageFactory.Attachment(exampleCards.CreateAudioCard().ToAttachment(), "Este é o AudioCard!!!"), cancellationToken);
                    break;
                case "VideoCard":
                    ret = stepContext.Context.SendActivityAsync(MessageFactory.Attachment(exampleCards.CreateVideoCard().ToAttachment(), "Este é o VideoCard!!!"), cancellationToken);
                    break;
                case "AnimationCard":
                    ret = stepContext.Context.SendActivityAsync(MessageFactory.Attachment(exampleCards.CreateAnimationCard().ToAttachment(), "Este é o AnimationCard!!!"), cancellationToken);
                    break;
                case "CarouselCard":
                    ret = stepContext.Context.SendActivityAsync(MessageFactory.Attachment(exampleCards.CreateCarouselCard(), "Este é o CarouselCard!!!"), cancellationToken);
                    break;
                default:
                    ret = stepContext.Context.SendActivityAsync(MessageFactory.Text("Não entendi qual Card vcoê deseja visualizar, pode digitar novamente?"), cancellationToken);
                    break;
            }

            return ret;
        }
    }
}
