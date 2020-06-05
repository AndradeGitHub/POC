using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Schema;

namespace MyFirstEchoBot.Cards
{
    public class ExampleCards
    {
        public HeroCard CreateHeroCard()
        {
            var heroCard = new HeroCard();
            heroCard.Title = "Título";
            heroCard.Subtitle = "SubTítulo";
            heroCard.Images = new List<CardImage>
                {
                    new CardImage("C:\\Users\\andre.de.andrade\\source\\repos\\Chatbot\\MyFirstEchoBot\\MyFirstEchoBot\\media\\planeta.jpg", "HeroCard", new CardAction(ActionTypes.OpenUrl, "Microsoft", value: "https://www.microsoft.com"))
                };
            heroCard.Buttons = new List<CardAction>
                {
                    new CardAction
                    {
                        Title = "Clique Aqui",
                        Type = ActionTypes.OpenUrl,
                        Value = "https://www.microsoft.com"
                    }
                };

            return heroCard;
        }

        public AudioCard CreateAudioCard()
        {
            var audioCard = new AudioCard();
            audioCard.Title = "Título";
            audioCard.Subtitle = "SubTítulo";
            audioCard.Autostart = true;
            audioCard.Autoloop = false;
            audioCard.Media = new List<MediaUrl>
                {
                    new MediaUrl("C:\\Users\\andre.de.andrade\\source\\repos\\Chatbot\\MyFirstEchoBot\\MyFirstEchoBot\\media\\bell-ringing-01.mp3", "Audio")
                };

            return audioCard;
        }

        public VideoCard CreateVideoCard()
        {
            var videoCard = new VideoCard();
            videoCard.Title = "Título";
            videoCard.Subtitle = "SubTítulo";
            videoCard.Autostart = true;
            videoCard.Autoloop = false;
            videoCard.Media = new List<MediaUrl>
                {
                    new MediaUrl("C:\\Users\\andre.de.andrade\\source\\repos\\Chatbot\\MyFirstEchoBot\\MyFirstEchoBot\\media\\171124_H1_005.mp4", "Video")
                };

            return videoCard;
        }

        public AnimationCard CreateAnimationCard()
        {
            var animationCard = new AnimationCard();
            animationCard.Title = "Título";
            animationCard.Subtitle = "SubTítulo";
            animationCard.Autostart = true;
            animationCard.Autoloop = false;
            animationCard.Media = new List<MediaUrl>
                {
                    new MediaUrl("C:\\Users\\andre.de.andrade\\source\\repos\\Chatbot\\MyFirstEchoBot\\MyFirstEchoBot\\media\\ezgif.com-resize_2.gif", "Animation")
                };

            return animationCard;
        }

        public List<Attachment> CreateCarouselCard()
        {
            var lstCarouselCard = new List<Attachment>();

            var heroCard = CreateHeroCard();
            Attachment attHeroCard = heroCard.ToAttachment();
            lstCarouselCard.Add(attHeroCard);

            var audioCard = CreateAudioCard();
            Attachment attAudioCard = audioCard.ToAttachment();
            lstCarouselCard.Add(attAudioCard);

            var videoCard = CreateVideoCard();
            Attachment attVideoCard = videoCard.ToAttachment();
            lstCarouselCard.Add(attVideoCard);

            var animationCard = CreateAnimationCard();
            Attachment attAnimationCard = animationCard.ToAttachment();
            lstCarouselCard.Add(attAnimationCard);

            return lstCarouselCard;
        }
    }
}
