using System;

namespace Models
{
    public class GameSettings
    {
        public Int32 MaxFinishedZombies { get; set; }
        public Int32 MoneyOnStart { get; set; }
        public Int32 PeaStepWidth { get; set; }
        public Int32 PeaDamage { get; set; }
        public Int32 ZombieCostOfDestroyed { get; set; }
        public Int32 ZombieStepWidth { get; set; }
    
        //game field parameters
        public Int32 TopShift { get; set; }
        public Int32 BorderWidth { get; set; }
        public Int32 SquareWidth { get; set; }
        public Int32 SquareHeight { get; set; }
        public Int32 LinesQuantity { get; set; }
        public Int32 VLinesQuantity { get; set; }

        //timers
        public Int32 MainInterval { get; set; }
        public Int32 ZombiesCreateInterval { get; set; }

        //colors
        public String BackgroundColor { get; set; }
        public String BorderColor { get; set; }
        public String TextColor { get; set; }
        public String FontName { get; set; }
        public String DeskColor1 { get; set; }
        public String DeskColor2 { get; set; }

        //captions
        public String StartText { get; set; }
        public String StartedText { get; set; }
        public Int32 StartCaptionX { get; set; }
        public Int32 StartCaptionY { get; set; }
        public Int32 StartCaptionWidth { get; set; }
        public Int32 StartCaptionHeight { get; set; }

        public String ZombiesFinishedText { get; set; }
        public Int32 ZombiesFinishedCaptionX { get; set; }
        public Int32 ZombiesFinishedCaptionY { get; set; }
        public Int32 ZombiesFinishedX { get; set; }
        public Int32 ZombiesFinishedY { get; set; }

        public String MoneyText { get; set; }
        public Int32 MoneyCaptionX { get; set; }
        public Int32 MoneyCaptionY { get; set; }
        public Int32 MoneyX { get; set; }
        public Int32 MoneyY { get; set; }

        public String ZombiesDestroyedText { get; set; }
        public Int32 ZombiesDestroyedCaptionX { get; set; }
        public Int32 ZombiesDestroyedCaptionY { get; set; }
        public Int32 ZombiesDestroyedX { get; set; }
        public Int32 ZombiesDestroyedY { get; set; }
    
        //Image paths
        public String ZombieImgSrc { get; set; }
        public String PeaCanonImgSrc { get; set; }
        public String PeaImgSrc { get; set; }
    }

    public class GameResultViewModel
    {
        public Int32 Id { get; set; }
        public Int32 UserId { get; set; }
        public String UserName { get; set; }
        public Int32 DestroyedZombies { get; set; }
        public DateTime GameDate { get; set; }
        public GameSettings Settings { get; set; }
    }
}