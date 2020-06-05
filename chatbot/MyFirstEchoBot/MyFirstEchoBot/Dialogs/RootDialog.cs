
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;

namespace MyFirstEchoBot.Dialogs
{
    public class RootDialog : ActivityHandler
    {
        //protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        //{
        //    foreach (var member in membersAdded)
        //    {
        //        if (member.Id != turnContext.Activity.Recipient.Id)
        //        {
        //            var welcomeCard = CreateAdaptiveCardAttachment();
        //            var response = MessageFactory.Attachment(welcomeCard);
        //            await turnContext.SendActivityAsync(response, cancellationToken);

        //            await turnContext.SendActivityAsync(MessageFactory.Text("Olá, eu sou o seu Robô Assistente! Em que posso ajudá-lo?"), cancellationToken);                  
        //        }
        //    }
        //}

        //// Load attachment from embedded resource.
        //private Attachment CreateAdaptiveCardAttachment()
        //{
        //    string[] paths1 = { "Cards", "welcomeCard.json" };
        //    var adaptiveCardJson1 = File.ReadAllText(Path.Combine(paths1));

        //    var adaptiveCardAttachment = new Attachment()
        //    {
        //        ContentType = "application/vnd.microsoft.card.adaptive",
        //        Content = JsonConvert.DeserializeObject(adaptiveCardJson1),
        //    };

        //    return adaptiveCardAttachment;
        //}

        //protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        //{
        //    //var textTypeUser = HandleSystemMessage(turnContext.Activity.Type);

        //    var textUser = turnContext.Activity.Text;  

        //    await CardTypeUser(textUser, turnContext, cancellationToken);
        //}

        //private string HandleSystemMessage(string type)
        //{
        //    var text = string.Empty;

        //    switch (type)
        //    {
        //        case ActivityTypes.DeleteUserData:

        //            break;
        //        case ActivityTypes.ConversationUpdate:

        //            break;
        //        case ActivityTypes.ContactRelationUpdate:

        //            break;
        //        case ActivityTypes.Typing:
        //            text = "Está demorando para responder, ainda está ae?";
        //            break;
        //    }

        //    return text;
        //}

        //private async Task CardTypeUser(string cardType, ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        //{
        //    if (cardType.Equals("HeroCard"))
        //    {
        //        var heroCard = CreateHeroCard();

        //        await turnContext.SendActivityAsync(MessageFactory.Attachment(heroCard.ToAttachment(), "Este é o HeroCard!!!"), cancellationToken);
        //        await turnContext.SendActivityAsync(MessageFactory.Text("Gostou do HeroCard?"), cancellationToken);
        //    }
        //    else if (cardType.Equals("AudioCard"))
        //    {
        //        var audioCard = CreateAudioCard();

        //        await turnContext.SendActivityAsync(MessageFactory.Attachment(audioCard.ToAttachment(), "Este é o AudioCard!!!"), cancellationToken);
        //    }
        //    else if (cardType.Equals("VideoCard"))
        //    {
        //        var videoCard = CreateVideoCard();

        //        await turnContext.SendActivityAsync(MessageFactory.Attachment(videoCard.ToAttachment(), "Este é o VideoCard!!!"), cancellationToken);
        //    }
        //    else if (cardType.Equals("AnimationCard"))
        //    {
        //        var animationCard = CreateAnimationCard();

        //        await turnContext.SendActivityAsync(MessageFactory.Attachment(animationCard.ToAttachment(), "Este é o AnimationCard!!!"), cancellationToken);
        //    }
        //    else if (cardType.Equals("CarouselCard"))
        //    {
        //        var carouselCard = CreateCarouselCard();

        //        await turnContext.SendActivityAsync(MessageFactory.Carousel(carouselCard, "Este é o CarouselCard!!!"), cancellationToken);
        //    }
        //    else
        //        await turnContext.SendActivityAsync(MessageFactory.Text("Não entendi, pode digitar novamente?"), cancellationToken);
        //}

        //private HeroCard CreateHeroCard()
        //{
        //    var heroCard = new HeroCard();
        //    heroCard.Title = "Título";
        //    heroCard.Subtitle = "SubTítulo";
        //    heroCard.Images = new List<CardImage>
        //    {
        //        new CardImage("C:\\Users\\andre.de.andrade\\source\\repos\\Chatbot\\MyFirstEchoBot\\MyFirstEchoBot\\media\\planeta.jpg", "HeroCard")
        //    };

        //    return heroCard;
        //}

        //private AudioCard CreateAudioCard()
        //{
        //    var audioCard = new AudioCard();
        //    audioCard.Title = "Título";
        //    audioCard.Subtitle = "SubTítulo";
        //    audioCard.Autostart = true;
        //    audioCard.Autoloop = false;
        //    audioCard.Media = new List<MediaUrl>
        //    {
        //        new MediaUrl("C:\\Users\\andre.de.andrade\\source\\repos\\Chatbot\\MyFirstEchoBot\\MyFirstEchoBot\\media\\bell-ringing-01.mp3", "Audio")
        //    };

        //    return audioCard;
        //}

        //private VideoCard CreateVideoCard()
        //{
        //    var videoCard = new VideoCard();
        //    videoCard.Title = "Título";
        //    videoCard.Subtitle = "SubTítulo";
        //    videoCard.Autostart = true;
        //    videoCard.Autoloop = false;
        //    videoCard.Media = new List<MediaUrl>
        //    {
        //        new MediaUrl("C:\\Users\\andre.de.andrade\\source\\repos\\Chatbot\\MyFirstEchoBot\\MyFirstEchoBot\\media\\171124_H1_005.mp4", "Video")
        //    };

        //    return videoCard;
        //}

        //private AnimationCard CreateAnimationCard()
        //{
        //    var animationCard = new AnimationCard();
        //    animationCard.Title = "Título";
        //    animationCard.Subtitle = "SubTítulo";
        //    animationCard.Autostart = true;
        //    animationCard.Autoloop = false;
        //    animationCard.Media = new List<MediaUrl>
        //    {
        //        new MediaUrl("C:\\Users\\andre.de.andrade\\source\\repos\\Chatbot\\MyFirstEchoBot\\MyFirstEchoBot\\media\\ezgif.com-resize_2.gif", "Animation")
        //    };

        //    return animationCard;
        //}

        //private List<Attachment> CreateCarouselCard()
        //{
        //    var lstCarouselCard = new List<Attachment>();

        //    var heroCard = CreateHeroCard();
        //    Attachment attHeroCard = heroCard.ToAttachment();
        //    lstCarouselCard.Add(attHeroCard);

        //    var audioCard = CreateAudioCard();
        //    Attachment attAudioCard = audioCard.ToAttachment();
        //    lstCarouselCard.Add(attAudioCard);

        //    var videoCard = CreateVideoCard();
        //    Attachment attVideoCard = videoCard.ToAttachment();
        //    lstCarouselCard.Add(attVideoCard);

        //    var animationCard = CreateAnimationCard();
        //    Attachment attAnimationCard = animationCard.ToAttachment();
        //    lstCarouselCard.Add(attAnimationCard);

        //    return lstCarouselCard;
        //}
    }
}
