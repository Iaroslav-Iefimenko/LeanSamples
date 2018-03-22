using BusinessLogic.Interfaces;
using Domain;
using Domain.Entities;
using System;
using System.Linq;

namespace BusinessLogic.Implementations
{
    public class EFGameSettingsRepository : IGameSettingsRepository
    {
        private EFDbContext context;

        public EFGameSettingsRepository(EFDbContext context)
        {
            this.context = context;
        }

        private Int32 GetInt32ValueByName(String name)
        {
            return Convert.ToInt32((from g in context.GameSettings
                    where g.Name == name
                    select g.Value).FirstOrDefault());
        }

        private String GetStringValueByName(String name)
        {
            return (from g in context.GameSettings
                    where g.Name == name
                    select g.Value).FirstOrDefault();
        }

        public int GetMaxFinishedZombies()
        {
            return GetInt32ValueByName("MaxFinishedZombies");
        }

        public int GetMoneyOnStart()
        {
            return GetInt32ValueByName("MoneyOnStart");
        }

        public int GetPeaStepWidth()
        {
            return GetInt32ValueByName("PeaStepWidth");
        }

        public int GetPeaDamage()
        {
            return GetInt32ValueByName("PeaDamage");
        }

        public int GetZombieCostOfDestroyed()
        {
            return GetInt32ValueByName("ZombieCostOfDestroyed");
        }

        public int GetZombieStepWidth()
        {
            return GetInt32ValueByName("ZombieStepWidth");
        }

        public int GetTopShift()
        {
            return GetInt32ValueByName("TopShift");
        }

        public int GetBorderWidth()
        {
            return GetInt32ValueByName("BorderWidth");
        }

        public int GetSquareWidth()
        {
            return GetInt32ValueByName("SquareWidth");
        }

        public int GetSquareHeight()
        {
            return GetInt32ValueByName("SquareHeight");
        }

        public int GetLinesQuantity()
        {
            return GetInt32ValueByName("LinesQuantity");
        }

        public int GetVLinesQuantity()
        {
            return GetInt32ValueByName("VLinesQuantity");
        }

        public int GetMainInterval()
        {
            return GetInt32ValueByName("MainInterval");
        }

        public int GetZombiesCreateInterval()
        {
            return GetInt32ValueByName("ZombiesCreateInterval");
        }

        public string GetBackgroundColor()
        {
            return GetStringValueByName("BackgroundColor");
        }

        public string GetBorderColor()
        {
            return GetStringValueByName("BorderColor");
        }

        public string GetTextColor()
        {
            return GetStringValueByName("TextColor");
        }

        public string GetFontName()
        {
            return GetStringValueByName("FontName");
        }

        public string GetDeskColor1()
        {
            return GetStringValueByName("DeskColor1");
        }

        public string GetDeskColor2()
        {
            return GetStringValueByName("DeskColor2");
        }

        public string GetStartText()
        {
            return GetStringValueByName("StartText");
        }

        public string GetStartedText()
        {
            return GetStringValueByName("StartedText");
        }

        public int GetStartCaptionX()
        {
            return GetInt32ValueByName("StartCaptionX");
        }

        public int GetStartCaptionY()
        {
            return GetInt32ValueByName("StartCaptionY");
        }

        public int GetStartCaptionWidth()
        {
            return GetInt32ValueByName("StartCaptionWidth");
        }

        public int GetStartCaptionHeight()
        {
            return GetInt32ValueByName("StartCaptionHeight");
        }

        public string GetZombiesFinishedText()
        {
            return GetStringValueByName("ZombiesFinishedText");
        }

        public int GetZombiesFinishedCaptionX()
        {
            return GetInt32ValueByName("ZombiesFinishedCaptionX");
        }

        public int GetZombiesFinishedCaptionY()
        {
            return GetInt32ValueByName("ZombiesFinishedCaptionY");
        }

        public int GetZombiesFinishedX()
        {
            return GetInt32ValueByName("ZombiesFinishedX");
        }

        public int GetZombiesFinishedY()
        {
            return GetInt32ValueByName("ZombiesFinishedY");
        }

        public string GetMoneyText()
        {
            return GetStringValueByName("MoneyText");
        }

        public int GetMoneyCaptionX()
        {
            return GetInt32ValueByName("MoneyCaptionX");
        }

        public int GetMoneyCaptionY()
        {
            return GetInt32ValueByName("MoneyCaptionY");
        }

        public int GetMoneyX()
        {
            return GetInt32ValueByName("MoneyX");
        }

        public int GetMoneyY()
        {
            return GetInt32ValueByName("MoneyY");
        }

        public string GetZombiesDestroyedText()
        {
            return GetStringValueByName("ZombiesDestroyedText");
        }

        public int GetZombiesDestroyedCaptionX()
        {
            return GetInt32ValueByName("ZombiesDestroyedCaptionX");
        }

        public int GetZombiesDestroyedCaptionY()
        {
            return GetInt32ValueByName("ZombiesDestroyedCaptionY");
        }

        public int GetZombiesDestroyedX()
        {
            return GetInt32ValueByName("ZombiesDestroyedX");
        }

        public int GetZombiesDestroyedY()
        {
            return GetInt32ValueByName("ZombiesDestroyedY");
        }

        public string GetZombieImgSrc()
        {
            return GetStringValueByName("ZombieImgSrc");
        }

        public string GetPeaCanonImgSrc()
        {
            return GetStringValueByName("PeaCanonImgSrc");
        }

        public string GetPeaImgSrc()
        {
            return GetStringValueByName("PeaImgSrc");
        }
    }
}