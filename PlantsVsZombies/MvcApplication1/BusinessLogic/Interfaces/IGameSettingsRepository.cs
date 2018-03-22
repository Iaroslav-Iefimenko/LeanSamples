using System;

namespace BusinessLogic.Interfaces
{
    public interface IGameSettingsRepository
    {
        Int32 GetMaxFinishedZombies();
        Int32 GetMoneyOnStart();
        Int32 GetPeaStepWidth();
        Int32 GetPeaDamage();
        Int32 GetZombieCostOfDestroyed();
        Int32 GetZombieStepWidth();

        //game field parameters
        Int32 GetTopShift();
        Int32 GetBorderWidth();
        Int32 GetSquareWidth();
        Int32 GetSquareHeight();
        Int32 GetLinesQuantity();
        Int32 GetVLinesQuantity();

        //timers
        Int32 GetMainInterval();
        Int32 GetZombiesCreateInterval();

        //colors
        String GetBackgroundColor();
        String GetBorderColor();
        String GetTextColor();
        String GetFontName();
        String GetDeskColor1();
        String GetDeskColor2();

        //captions
        String GetStartText();
        String GetStartedText();
        Int32 GetStartCaptionX();
        Int32 GetStartCaptionY();
        Int32 GetStartCaptionWidth();
        Int32 GetStartCaptionHeight();

        String GetZombiesFinishedText();
        Int32 GetZombiesFinishedCaptionX();
        Int32 GetZombiesFinishedCaptionY();
        Int32 GetZombiesFinishedX();
        Int32 GetZombiesFinishedY();

        String GetMoneyText();
        Int32 GetMoneyCaptionX();
        Int32 GetMoneyCaptionY();
        Int32 GetMoneyX();
        Int32 GetMoneyY();

        String GetZombiesDestroyedText();
        Int32 GetZombiesDestroyedCaptionX();
        Int32 GetZombiesDestroyedCaptionY();
        Int32 GetZombiesDestroyedX();
        Int32 GetZombiesDestroyedY();

        //Image paths
        String GetZombieImgSrc();
        String GetPeaCanonImgSrc();
        String GetPeaImgSrc(); 
    }
}