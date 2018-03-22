//базовый класс графических объектов
function GraphicObject() {
    this.LineNum = 0;
    this.X = 0;
    this.Y = 0;
    this.Width = 0;
    this.Height = 0;
    this.Img = 0;
    this.GoToRight = true;
    this.StepWidth = 0;
    this.IsFinished = false;
}

GraphicObject.prototype.Draw = function (canvasContext) {
    canvasContext.drawImage(this.Img, this.X, this.Y, this.Width, this.Height);
};

GraphicObject.prototype.DoStep = function (canvas, borderWidth) {
    if (this.GoToRight && this.X > canvas.width - this.Width - this.StepWidth - borderWidth) {
        this.IsFinished = true;
        return;
    }

    if (!this.GoToRight && this.X < this.StepWidth + borderWidth) {
        this.IsFinished = true;
        return;
    }

    if (this.GoToRight)
        this.X = this.X + this.StepWidth;
    else 
        this.X = this.X - this.StepWidth;
};