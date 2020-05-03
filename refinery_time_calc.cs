void Main()
        {
            //Input your world speed in place of 1
            var worldSpeed = 1;
            //Include CALC in the display's name
            //Getting all refineries
            var refs = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyRefinery>(refs);
            if (refs.Count == 0)
                return;

            //Getting all screens
            var screens = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(screens);
            if (screens.Count == 0)
                return;
            var calcScreens = new List<IMyTerminalBlock>();
            for (int i = 0; i < screens.Count; i++)
            {
                var screen = screens[i];
                if (screen.CustomName.Contains("CALC"))
                    calcScreens.Add(screen);
            }
            if (calcScreens.Count == 0)
                return;

            //Set up variables
            var ratioStone = 0.9f;
            var ratioScrap = 0.8f;
            var ratioIron = 0.7f;
            var ratioSilicon = 0.7f;
            var ratioNickel = 0.4f;
            var ratioCobalt = 0.3f;
            var ratioSilver = 0.1f;
            var ratioGold = 0.01f;
            var ratioMagnesium = 0.007f;
            var ratioUranium = 0.007f;
            var ratioPlatinum = 0.005f;

            var inputStoneRaw = 46800;
            var inputScrapRaw = 117000;
            var inputIronRaw = 93600;
            var inputSiliconRaw = 7800;
            var inputNickelRaw = 2340;
            var inputCobaltRaw= 1170;
            var inputSilverRaw = 4680;
            var inputGoldRaw = 11700;
            var inputMagnesiumRaw = 4680;
            var inputUraniumRaw = 1170;
            var inputPlatinumRaw = 1170;

            var ratioBase = 0.8f;
            var speedBase = 1.3f;


        }