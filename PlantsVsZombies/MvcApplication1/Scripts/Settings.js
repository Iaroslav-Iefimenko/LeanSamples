//класс настроек
function Settings() {
    //game parameters
    this.MaxFinishedZombies = 20;
    this.MoneyOnStart = 525;
    this.PeaStepWidth = 30;
    this.PeaDamage = 10;
    this.ZombieCostOfDestroyed = 30;
    this.ZombieStepWidth = 60;

    this.ZombieHealth = 100;
    this.PeaCanonCost = 75;
    
    //game field parameters
    this.TopShift = 100;
    this.BorderWidth = 10;
    this.SquareWidth = 100;
    this.SquareHeight = 100;
    this.LinesQuantity = 5;
    this.VLinesQuantity = 8;
    this.CanvasWidth = 0;
    this.CanvasHeight = 0;

    //timers
    this.MainInterval = 500;
    this.ZombiesCreateInterval = 2000;

    //colors
    this.BackgroundColor = "black";
    this.BorderColor = "#FF6A00";
    this.TextColor = "#FF0000";
    this.FontName = "14px Arial";
    this.DeskColor1 = "#00FF21";
    this.DeskColor2 = "#007F46";

    //captions
    this.StartText = "Press to start!!!";
    this.StartedText = "Game is started!!!";
    this.StartCaptionX = 20;
    this.StartCaptionY = 30;
    this.StartCaptionWidth = 100;
    this.StartCaptionHeight = 30;
    
    this.ZombiesFinishedText = "Zombies finished: ";
    this.ZombiesFinishedCaptionX = 600;
    this.ZombiesFinishedCaptionY = 25;
    this.ZombiesFinishedX = 750;
    this.ZombiesFinishedY = 25;
    
    this.MoneyText = "Money: ";
    this.MoneyCaptionX = 600;
    this.MoneyCaptionY = 45;
    this.MoneyX = 750;
    this.MoneyY = 45;
    
    this.ZombiesDestroyedText = "Zombies destroyed: ";
    this.ZombiesDestroyedCaptionX = 600;
    this.ZombiesDestroyedCaptionY = 65;
    this.ZombiesDestroyedX = 750;
    this.ZombiesDestroyedY = 65;
    
    //Image paths
    this.ZombieImgSrc = "../Images/Zombie1.gif";
    this.ZombieImgWidth = 60;
    this.ZombieImgHeight = 90;
    this.PeaCanonImgSrc = "../Images/gun.gif";
    this.PeaCanonImgWidth = 60;
    this.PeaCanonImgHeight = 60;
    this.PeaImgSrc = "../Images/gorox.gif";
    this.PeaImgWidth = 20;
    this.PeaImgHeight = 20;
}