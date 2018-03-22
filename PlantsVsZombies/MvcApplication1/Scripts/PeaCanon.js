//класс пушка

function PeaCanon(lineNum, img, s) {
    this.Active = false;
    this.LineNum = lineNum;
    this.VLineNum = 0;
    if (lineNum < 0)
        this.X = PeaCanon.prototype.ShiftX - lineNum * (s.BorderWidth + s.PeaCanonImgWidth);
    else
        this.X = s.BorderWidth + s.SquareWidth/2 - s.PeaCanonImgWidth/2 + s.SquareWidth * (this.VLineNum - 1);
    if (lineNum < 0)
        this.Y = PeaCanon.prototype.ShiftY;
    else
        this.Y = s.TopShift + 2 * s.BorderWidth + s.SquareHeight * (this.LineNum - 1);
    this.Width = s.PeaCanonImgWidth;
    this.Height = s.PeaCanonImgHeight;
    this.Img = img;
    this.Settings = s;
}
extend(PeaCanon, GraphicObject);

PeaCanon.prototype.PeaCanon1X = 200;
PeaCanon.prototype.PeaCanon2X = 270;
PeaCanon.prototype.PeaCanon3X = 340;
PeaCanon.prototype.PeaCanon1Y = 20;
PeaCanon.prototype.ShiftX = 130;
PeaCanon.prototype.ShiftY = 20;
PeaCanon.prototype.PeaImg = 0;

PeaCanon.prototype.Fire = function() {
    var pea = new Pea(this.LineNum, this.VLineNum,
        PeaCanon.prototype.PeaImg, this.Settings);
    return pea;
};

PeaCanon.prototype.SetLineNums = function(lNum, vlNum) {
    this.LineNum = lNum;
    this.VLineNum = vlNum;
    if (lNum < 0)
        this.X = PeaCanon.prototype.ShiftX - lNum * (this.Settings.BorderWidth + this.Width);
    else
        this.X = this.Settings.BorderWidth + this.Settings.SquareWidth/2 - this.Width/2 + this.Settings.SquareWidth * (this.VLineNum - 1);
    if (lNum < 0)
        this.Y = PeaCanon.prototype.ShiftY;
    else
        this.Y = this.Settings.TopShift + 2 * this.Settings.BorderWidth + this.Settings.SquareHeight * (this.LineNum - 1);
};