//класс пушка

function Pea(lineNum, vLineNum, img, settings) {
    this.LineNum = lineNum;
    this.X = settings.BorderWidth + settings.SquareWidth / 2 - settings.PeaImgWidth / 2
        + settings.SquareWidth * (vLineNum - 1);
    this.Y = settings.TopShift + 2*settings.BorderWidth + settings.SquareHeight * (this.LineNum - 1);
    this.Width = settings.PeaImgWidth;
    this.Height = settings.PeaImgHeight;
    this.Img = img;
    this.GoToRight = true;
    this.StepWidth = settings.PeaStepWidth;
}
extend(Pea, GraphicObject);