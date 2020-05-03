# Refinery Time Calculator Function
# Built to mock up the logic for Space Engineers. Need to Convert to C#.

# Set up variables


# Yield and Speed mods are the sockets filled (so for 4 mods you'd input 8)
# Refinery amount assumes every refinery has the same settings for the sake of simplicity
# Ratio is for Base Yield
# World Speed is the refinery speed in world settings

yieldMods = 0
speedMods = 0
refineryCount = 1
ratioBase = 0.8
speedBase = 1.3

# Conversion Ratios
ratioStone = 0.9
ratioScrap = 0.8
ratioIron = 0.7
ratioSilicon = 0.7
ratioNickel = 0.4
ratioCobalt = 0.3
ratioSilver = 0.1
ratioGold = 0.01
ratioMagnesium = 0.007
ratioUranium = 0.007
ratioPlatinum = 0.005

conversionRatios = [['Stone', ratioStone],['Scrap', ratioScrap],['Iron', ratioIron],['Silicon', ratioSilicon],['Nickel',ratioNickel],['Cobalt',ratioCobalt],['Silver',ratioSilver],['Gold', ratioGold],['Mag', ratioMagnesium],['Uranium', ratioUranium],['Platinum',ratioPlatinum]]

# Base Input Values (kg/h)
inputStoneRaw = 46800
inputScrapRaw = 117000
inputIronRaw = 93600
inputSiliconRaw = 7800
inputNickelRaw = 2340
inputCobaltRaw= 1170
inputSilverRaw = 4680
inputGoldRaw = 11700
inputMagnesiumRaw = 4680
inputUraniumRaw = 1170
inputPlatinumRaw = 1170

baseInputValues = [['Stone', inputStoneRaw], ['Scrap', inputScrapRaw],['Iron', inputIronRaw],['Silicon', inputSiliconRaw],['Nickle', inputNickelRaw],['Cobalt', inputCobaltRaw],['Silver', inputSilverRaw],['Gold', inputGoldRaw],['Mag', inputMagnesiumRaw],['Uranium', inputUraniumRaw],['Platinum', inputPlatinumRaw]]


# Logic

def calculateRefineryTime(ore, inputOreWeightActual):

    # Query lists to check for Base Values
    for i in range(len(baseInputValues)):
        if baseInputValues[i][0] == ore:
            inputOreWeightBase = baseInputValues[i][1]
            break
        else:
            inputOreWeightBase = 0

    for i in range(len(conversionRatios)):
        if conversionRatios[i][0] == ore:
            ratioOre = conversionRatios[i][1]
            break
        else:
            ratioOre = 0


    # Calculate Ratio
    if ratioOre * ratioBase * 10905077 ** yieldMods > 1:
        ratio = 1
    else:
        ratio = ratioOre * ratioBase * 10905077 ** yieldMods

    # Calculate Output in kg/t
    outputPer1000 = ratio * 1000

    # Calculate Input and Output in kg/h from Base Input Values
    inputOfOreBase = inputOreWeightBase * (speedBase + speedMods / 2) * refineryCount

    # Will be used if estimating desired Ore Refinery Time, currently not used.
    # outputOfOreBase = inputOfOreBase * outputPer1000

    # Calculate Total Output and Time
    outputOfOreActual = inputOreWeightActual * ratio
    timeToCompleteAll = (inputOreWeightActual / inputOfOreBase)

    hours = int(timeToCompleteAll)
    minutes = int(round(((timeToCompleteAll*60) % 60), 0))
    seconds = int(round(((timeToCompleteAll*3600) % 60), 0))
    
    if seconds < 10:
        seconds = f'0{str(seconds)}'
    elif seconds == 60:
        seconds = '00'
        minutes += 1

    if minutes < 10:
        minutes = f'0{str(minutes)}'

    if hours < 10:
        hours = f'0{str(hours)}'


    timeToCompleteStr = f'{hours}:{minutes}:{seconds}'

    print(f'''Conversion Ratio for {ore} is {int(outputPer1000)}/1000kg
Time to complete is {timeToCompleteStr}
    ''')

    return outputOfOreActual, outputPer1000, timeToCompleteStr



calculateRefineryTime('Stone', 2000)


