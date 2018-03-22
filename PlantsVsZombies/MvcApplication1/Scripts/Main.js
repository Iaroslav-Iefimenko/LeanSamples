//класс игры
function PlantsVsZombiesGame(s) {
    var canvas = document.getElementById("myCanvas");
    //arays of objects
    var zombies = []; //array of zombies
    var peaCanons = []; //array of pea canons
    var peas = []; //array of peas

    //current game parameters
    var destroyedZombies = 0;
    var finishedZombies = 0;
    var gameIsStarted = false;

    //free peaCanon fields on top panels
    var peaCanon1Free = true;
    var peaCanon2Free = true;
    var peaCanon3Free = true;

    //drag'n'drop parameters
    var isDropNeeded = false;
    var isDropNeededNum = -1;

    var settings = s;
    var money = settings.MoneyOnStart;
    s.CanvasWidth = canvas.width;
    s.CanvasHeight = canvas.height;

    if (canvas.getContext) {
        canvas.onclick = canvasOnclick;
        canvas.onmousedown = canvasOnmousedown;
        canvas.onmousemove = canvasOnmousemove;
        canvas.onmouseup = canvasOnmouseup;
        // Specify 2d canvas type.
        var canvasContext = canvas.getContext("2d");

        //images
        var zombieImg = new Image();
        zombieImg.src = settings.ZombieImgSrc;
        var peaCanonImg = new Image();
        peaCanonImg.src = settings.PeaCanonImgSrc;
        PeaCanon.prototype.PeaImg = new Image();
        PeaCanon.prototype.PeaImg.src = settings.PeaImgSrc;

        dopeaCanonsCreate();

        //timers
        var gameLoop = 0;
        var zombiesCreateLoop = 0;
        var peaCanonsCreateLoop = setInterval(dopeaCanonsCreate, settings.MainInterval);
    }

    function canvasOnclick(e) {
        e = e || event;
        var x = e.pageX - canvas.offsetLeft;
        var y = e.pageY - canvas.offsetTop;

        if (!gameIsStarted
            && x > settings.StartCaptionX
            && x < settings.StartCaptionX + settings.StartCaptionWidth
            && y > settings.StartCaptionY - 10
            && y < settings.StartCaptionY + settings.StartCaptionHeight - 10) {
            // Play the game until the until the game is over.
            gameLoop = setInterval(doGameLoop, settings.MainInterval);
            zombiesCreateLoop = setInterval(doZombiesCreate, settings.ZombiesCreateInterval);

            gameIsStarted = true;
            redrawAll();
        }
    }

    function canvasOnmousedown(e) {
        e = e || event;
        var x = e.pageX - canvas.offsetLeft;
        var y = e.pageY - canvas.offsetTop;

        if (!peaCanon1Free
            && x > PeaCanon.prototype.PeaCanon1X
            && x < PeaCanon.prototype.PeaCanon1X + settings.PeaCanonImgWidth
            && y > PeaCanon.prototype.PeaCanon1Y
            && y < PeaCanon.prototype.PeaCanon1Y + settings.PeaCanonImgHeight) {
            isDropNeeded = true;
            isDropNeededNum = -1;
        }

        if (!peaCanon2Free
            && x > PeaCanon.prototype.PeaCanon2X
            && x < PeaCanon.prototype.PeaCanon2X + settings.PeaCanonImgWidth
            && y > PeaCanon.prototype.PeaCanon1Y
            && y < PeaCanon.prototype.PeaCanon1Y + settings.PeaCanonImgHeight) {
            isDropNeeded = true;
            isDropNeededNum = -2;
        }

        if (!peaCanon3Free
            && x > PeaCanon.prototype.PeaCanon3X
            && x < PeaCanon.prototype.PeaCanon3X + settings.PeaCanonImgWidth
            && y > PeaCanon.prototype.PeaCanon1Y
            && y < PeaCanon.prototype.PeaCanon1Y + settings.PeaCanonImgHeight) {
            isDropNeeded = true;
            isDropNeededNum = -3;
        }
    }

    function canvasOnmousemove(e) {
        e = e || event;
        var x = e.pageX - canvas.offsetLeft;
        var y = e.pageY - canvas.offsetTop;

        if (isDropNeeded) {
            var pozx = x - Math.floor(settings.PeaCanonImgWidth / 2);
            var pozy = y - Math.floor(settings.PeaCanonImgHeight / 2);
            // отлавливаем выход за пределы экрана
            if (pozx < 0) {
                pozx = 0;
            }
            if (pozy < 0) {
                pozy = 0;
            }
            if (pozx > canvas.width - settings.PeaCanonImgWidth) {
                pozx = canvas.width - settings.PeaCanonImgWidth;
            }
            if (pozy > canvas.height - settings.PeaCanonImgHeight) {
                pozy = canvas.height - settings.PeaCanonImgHeight;
            }
            redrawAll();
            canvasContext.drawImage(peaCanonImg, pozx, pozy,
                settings.PeaCanonImgWidth, settings.PeaCanonImgHeight);
        }
    }

    function canvasOnmouseup(e) {
        e = e || event;
        var x = e.pageX - canvas.offsetLeft;
        var y = e.pageY - canvas.offsetTop;
        var i;
        if (isDropNeeded) {
            isDropNeeded = false;
            //находим номера линий
            var lineNum = getLineNumFromY(y);
            var vlineNum = getVLineNumFromX(x);
            //проверяем, нет ли там пушки
            for (i = 0; i < peaCanons.length; i++) {
                if (peaCanons[i].LineNum == lineNum && peaCanons[i].VLineNum == vlineNum) {
                    redrawAll();
                    return;
                }
            }
            //находим перетаскиваемую пушку и устанавливаем ее на позицию
            for (i = 0; i < peaCanons.length; i++) {
                if (peaCanons[i].LineNum == isDropNeededNum) {
                    peaCanons[i].SetLineNums(lineNum, vlineNum);
                    break;
                }
            }

            //освобождаем ее место наверху
            switch (isDropNeededNum) {
            case -1:
                peaCanon1Free = true;
                break;
            case -2:
                peaCanon2Free = true;
                break;
            case -3:
                peaCanon3Free = true;
                break;
            }
            redrawAll();
        }
    }

    function doGameLoop() {
        var i;
        for (i = 0; i < zombies.length; i++) {
            //шаг
            zombies[i].DoStep(canvas, settings.BorderWidth);
            var x = zombies[i].X;
            var y = zombies[i].Y;
            //финиш
            if (zombies[i].IsFinished) {
                zombies.splice(i, 1);
                finishedZombies++;
                i--;
                continue;
            }
            //находим номера линий
            var lineNum = getLineNumFromY(y);
            var vlineNum = getVLineNumFromX(x);
            //ищем пушку
            for (var z = 0; z < peaCanons.length; z++) {
                if (peaCanons[z].LineNum == lineNum && peaCanons[z].VLineNum == vlineNum) {
                    peaCanons.splice(z, 1);
                    break;
                }
            }
        }

        for (i = 0; i < peaCanons.length; i++) {
            peas.push(peaCanons[i].Fire());
        }

        for (i = 0; i < peas.length; i++) {
            peas[i].DoStep(canvas, settings.BorderWidth);
            for (var j = 0; j < zombies.length; j++) {
                //проверяем столкновения с зомби
                if (zombies[j].X < peas[i].X && zombies[j].LineNum == peas[i].LineNum) {
                    zombies[j].Health = zombies[j].Health - settings.PeaDamage;
                    peas.splice(i, 1);
                    i--;

                    if (zombies[j].Health <= 0) {
                        zombies.splice(j, 1);
                        destroyedZombies++;
                        money = money + settings.ZombieCostOfDestroyed;
                    }
                    break;
                }
                //финиш
                if (peas[i].IsFinished) {
                    peas.splice(i, 1);
                    i--;
                    continue;
                }
            }
        }

        if (finishedZombies > settings.MaxFinishedZombies) {
            clearTimeout(gameLoop);
            clearTimeout(zombiesCreateLoop);
            clearTimeout(peaCanonsCreateLoop);
            zombies.length = 0;
            peaCanons.length = 0;
            peas.length = 0;

            window.location.href = '/Home/AddResult?destroyedZombies=' + destroyedZombies;
        }

        redrawAll();
    }

    function doZombiesCreate() {
        var min = 1, max = settings.LinesQuantity;
        var rand = min - 0.5 + Math.random() * (max - min + 1);
        rand = Math.round(rand);

        var z1 = new Zombie(rand, zombieImg, settings);
        zombies.push(z1);
    }

    function dopeaCanonsCreate() {
        if (peaCanon1Free && money >= settings.PeaCanonCost) {
            var peaCanon = new PeaCanon(-1, peaCanonImg, settings);
            peaCanon.Draw(canvasContext);
            money = money - settings.PeaCanonCost;
            peaCanon1Free = false;
            peaCanons.push(peaCanon);
        }

        if (peaCanon2Free && money >= settings.PeaCanonCost) {
            var peaCanon2 = new PeaCanon(-2, peaCanonImg, settings);
            peaCanon2.Draw(canvasContext);
            money = money - settings.PeaCanonCost;
            peaCanon2Free = false;
            peaCanons.push(peaCanon2);
        }

        if (peaCanon3Free && money >= settings.PeaCanonCost) {
            var peaCanon3 = new PeaCanon(-3, peaCanonImg, settings);
            peaCanon3.Draw(canvasContext);
            money = money - settings.PeaCanonCost;
            peaCanon3Free = false;
            peaCanons.push(peaCanon3);
        }

        redrawAll();
    }

    function drawBorder() {
        // Paint it black.
        canvasContext.fillStyle = settings.BorderColor;
        canvasContext.fillRect(0, 0, canvas.width, canvas.height);
        canvasContext.fillStyle = settings.BackgroundColor;
        canvasContext.fillRect(settings.BorderWidth, settings.BorderWidth,
            canvas.width - 2 * settings.BorderWidth, canvas.height - 2 * settings.BorderWidth);

        canvasContext.fillStyle = settings.BorderColor;
        canvasContext.fillRect(0, settings.TopShift - settings.BorderWidth, canvas.width, settings.BorderWidth);
    }

    function drawDesk() {
        for (var i = 0; i < settings.LinesQuantity; i++)
            for (var j = 0; j < settings.VLinesQuantity; j++) {
                if ((i + j) % 2 == 0)
                    canvasContext.fillStyle = settings.DeskColor1;
                else
                    canvasContext.fillStyle = settings.DeskColor2;
                var height = settings.SquareHeight;
                var width = settings.SquareWidth;
                var pW = settings.BorderWidth + j * settings.SquareWidth;
                var pH = settings.TopShift + i * settings.SquareHeight;
                if (pW + width > canvas.width - settings.BorderWidth)
                    width = canvas.width - settings.BorderWidth - pW;
                if (pH + height > canvas.height - settings.BorderWidth)
                    height = canvas.height - settings.BorderWidth - pH;
                canvasContext.fillRect(pW, pH, width, height);
            }
    }

    function drawCaptions() {
        canvasContext.font = settings.FontName;
        canvasContext.fillStyle = settings.TextColor;

        if (!gameIsStarted)
            canvasContext.fillText(settings.StartText, settings.StartCaptionX, settings.StartCaptionY);
        else
            canvasContext.fillText(settings.StartedText, settings.StartCaptionX, settings.StartCaptionY);

        canvasContext.fillText(settings.ZombiesFinishedText, settings.ZombiesFinishedCaptionX, settings.ZombiesFinishedCaptionY);
        canvasContext.fillText(settings.MoneyText, settings.MoneyCaptionX, settings.MoneyCaptionY);
        canvasContext.fillText(settings.ZombiesDestroyedText, settings.ZombiesDestroyedCaptionX, settings.ZombiesDestroyedCaptionY);

        canvasContext.fillText(finishedZombies, settings.ZombiesFinishedX, settings.ZombiesFinishedY);
        canvasContext.fillText(money, settings.MoneyX, settings.MoneyY);
        canvasContext.fillText(destroyedZombies, settings.ZombiesDestroyedX, settings.ZombiesDestroyedY);
    }

    function redrawAll() {
        drawBorder();
        drawDesk();
        drawCaptions();
        var i;
        for (i = 0; i < zombies.length; i++)
            zombies[i].Draw(canvasContext);
        for (i = 0; i < peaCanons.length; i++)
            peaCanons[i].Draw(canvasContext);
        for (i = 0; i < peas.length; i++)
            peas[i].Draw(canvasContext);
    }

    function getLineNumFromY(y) {
        return Math.floor((y - settings.TopShift) / settings.SquareHeight) + 1;
    }

    function getVLineNumFromX(x) {
        return Math.floor((x - settings.BorderWidth) / settings.SquareWidth) + 1;
    }
}