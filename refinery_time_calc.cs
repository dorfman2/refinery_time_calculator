using Sandbox.Game.GameSystems;
using System;
using System.Collections.Generic;
using Sandbox.Common;
using Sandbox.ModAPI;
 
namespace Refinery_Time_Calculator
{
    class Program
    {
 
        // Refinery Time Calculator Function
        // Built to mock up the logic for Space Engineers. Need to Convert to C//.
        // Set up variables
        // Yield and Speed mods are the sockets filled (so for 4 mods you"d input 8)
        // Refinery amount assumes every refinery has the same settings for the sake of simplicity
        // Ratio is for Base Yield
        // World Speed is the refinery speed in world settings
 
        //double yieldMods = 0;
        //double speedMods = 0;
        //double refineryCount = 1;
        //double ratioBase = 0.8;
        //double speedBase = 1.3;
        //double ratioStone = 0.9;
        //double ratioScrap = 0.8;
 
        // Convertion Ratios
 
        //public static IMyGridTerminalSystem GridTerminalSystem = null; Dont Remove comments UNLESS YOUR IN IDE
 
        static double yieldMods = 0;
        static double speedMods = 0;
        static double refineryCount = 1;
        static double ratioBase = 0.8;
        static double speedBase = 1.3;
 
        static double ratioStone = 0.9;
        static double ratioScrap = 0.8;
        static double ratioIron = 0.7;
        static double ratioSilicon = 0.7;
        static double ratioNickel = 0.4;
        static double ratioCobalt = 0.3;
        static double ratioSilver = 0.1;
        static double ratioGold = 0.01;
        static double ratioMagnesium = 0.007;
        static double ratioUranium = 0.007;
        static double ratioPlatinum = 0.005;
 
        static Dictionary<string, double> conversionRatios = new Dictionary<string, double>
        {
            { "stone", ratioStone}, { "scrap", ratioScrap},{ "iron", ratioIron},{ "silicon", ratioSilicon},
            { "nickel", ratioNickel},{ "cobalt", ratioCobalt},{ "silver", ratioSilver},{ "gold", ratioGold},
            { "Mag", ratioMagnesium},{ "uranium", ratioUranium},{ "platinum", ratioPlatinum}
        };
 
        // Base Input Values (kg/h)
        static double inputStoneRaw = 46800;
        static double inputScrapRaw = 117000;
        static double inputIronRaw = 93600;
        static double inputSiliconRaw = 7800;
        static double inputNickelRaw = 2340;
        static double inputCobaltRaw = 1170;
        static double inputSilverRaw = 4680;
        static double inputGoldRaw = 11700;
        static double inputMagnesiumRaw = 4680;
        static double inputUraniumRaw = 1170;
        static double inputPlatinumRaw = 1170;
 
 
        static Dictionary<string, double> baseInputValues = new Dictionary<string, double>
        {
            {"stone", inputStoneRaw}, {"scrap", inputScrapRaw},{"iron", inputIronRaw},{"silicon", inputSiliconRaw},
            { "nickle", inputNickelRaw},{"cobalt", inputCobaltRaw},{"silver", inputSilverRaw},{"gold", inputGoldRaw},
            { "mag", inputMagnesiumRaw},{"uranium", inputUraniumRaw},{"platinum", inputPlatinumRaw}
        };
 
 
        public static void Main(string input)
        {
            /// so a string is passed seperated by a comma... the ore name is first and its weight second...
            // for example, in the arguments tab, you would type --> stone, 2000
            String[] data = input.Split(",");
 
            // This will crash if comma is NOT IN ARGUMENTS!!!!!
            string oreName = data[0];
            double weight = double.Parse(data[1]);
 
            oreName = oreName.ToLower(); // since strings with different capitalizations can cause errors, ALL Strings will be lower case
            calculateRefineryTime(oreName, weight);
        }
 
        public static void calculateRefineryTime(string ore, double inputOreWeightActual)
        {
            double inputOreWeightBase = 0;
            double ratioOre = 0;
 
            double outputPer1000;
            double ratio;
 
            baseInputValues.TryGetValue(ore, out inputOreWeightBase); // gets value using key and puts value in inputorebase variable if the key exists
 
            conversionRatios.TryGetValue(ore, out ratioOre);
 
            if (Math.Pow((ratioOre * ratioBase * 10905077), yieldMods) > 1)
            {
                ratio = 1;
            }
            else
            {
                ratio = Math.Pow((ratioOre * ratioBase * 10905077), yieldMods);
            }
 
            //Calculate Output in kg/t
            outputPer1000 = ratio * 1000;
 
            //Calculate Input and Output in kg/h from Base Input Values
 
            double inputofOreBase = inputOreWeightBase * (speedBase + (speedMods / 2)) * refineryCount; // poss error -- middle paranthesis -- order of precedence
 
            //Will be used if estimating desired Ore Refinery Time, currently not used.
            // outputOfOreBase = inputOfOreBase * outputPer1000
 
            // Calculate Total output and Time
 
            double outputOfOreActual = inputOreWeightActual * ratio;
 
            double timeToCompleteAll = (inputOreWeightActual / inputofOreBase);
 
            int hours = (int)timeToCompleteAll;
 
            int minutes = (int)Math.Floor(((timeToCompleteAll * 60) % 60)); // poss error, you rounded to 0 here, im not sure why... this rounds to next least integer -- so a 6.023 becomes a 6
 
            int seconds = (int)Math.Floor(((timeToCompleteAll * 3600) % 60)); // poss error, read above
 
            if(seconds < 10)
            {
                seconds = int.Parse("0" + seconds); // adds a zero infront of the number so its in 00 format            
            }
            else if(seconds == 60)
            {
                seconds = 0;
                minutes += 1;
            }
 
            if(hours < 10)
            {
                hours = int.Parse("0" + hours); // adds a zero infront of the number so its in 00 format
            }
 
            String timeToCompleteStr = hours + ":" + minutes + ":" + seconds; // c# auto converts numbers to String
 
            String text2Write = "Conversion Ratio for " + ore + " is " + outputPer1000 + "/1000kg. Time to complete is " + timeToCompleteStr;
 
            IMyTextPanel displayTime = GridTerminalSystem.GetBlockWithName("RefineryTimeDisplay") as IMyTextPanel;
 
            displayTime.WritePublicTitle(text2Write + "\n it worked!!!");
 
        }
    }
}
