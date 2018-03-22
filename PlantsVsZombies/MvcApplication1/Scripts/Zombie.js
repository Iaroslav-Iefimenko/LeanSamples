//класс зомби

//конструктор
function Zombie(lineNum, img, settings) {
    this.LineNum = lineNum;
    this.X = settings.CanvasWidth - settings.BorderWidth - settings.ZombieImgWidth; 
    this.Y = settings.TopShift + settings.SquareHeight * (this.LineNum - 1); 
    this.Health = settings.ZombieHealth;
    this.ZombieDestroyed = false;
    this.Width = settings.ZombieImgWidth;
    this.Height = settings.ZombieImgHeight;
    this.Img = img;
    this.GoToRight = false;
    this.StepWidth = settings.ZombieStepWidth;
}
extend(Zombie, GraphicObject);