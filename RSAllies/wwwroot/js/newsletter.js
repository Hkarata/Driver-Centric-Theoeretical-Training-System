class NewsArticle {
    constructor(title, stitle, content, scontent, date, sdate) {
        this.title = title;
        this.stitle = stitle;
        this.content = content;
        this.scontent = scontent;
        this.date = date;
        this.sdate = sdate;
    }
}


const newsList = [];

// Article 1: Pedestrian Safety Campaign Launched
newsList.push(new NewsArticle(
    "Pedestrian Safety Campaign Launched in Dar es Salaam.",
    "Kampeni ya Usalama kwa Watembea kwa miguu Yazinduliwa Jijini Dar es Salaam.",
    "A new campaign aimed at raising awareness of pedestrian safety has been launched in [City Name]. The campaign, titled 'Look Out, Stay Safe,' will focus on educating both pedestrians and drivers on how to share the road safely. Initiatives include public service announcements, educational workshops, and increased enforcement of crosswalk laws. The goal of the campaign is to reduce the number of pedestrian accidents in Dar es Salaam.",
    "Kampeni mpya yenye lengo la kuongeza uelewa wa usalama wa watembea kwa miguu imezinduliwa katika [Jina la Jiji]. Kampeni hiyo iliyopewa jina la 'Look Out, Stay Safe' italenga kutoa elimu kwa watembea kwa miguu na madereva jinsi ya kushiriki barabara kwa usalama. Mipango ni pamoja na kuwaelimisha watembea kwa miguu na madereva. matangazo ya utumishi wa umma, warsha za elimu, na kuongezeka kwa utekelezaji wa sheria za njia panda Lengo la kampeni ni kupunguza idadi ya ajali za watembea kwa miguu jijini Dar es Salaam.",
    "19 May 2024",
    "19 Mei 2024"
    ));

// Article 2: Cyclist Safety Tips for Sharing the Road
newsList.push(new NewsArticle(
    "Cyclist Safety Tips for Sharing the Road with Cars.",
    "Vidokezo vya Usalama vya Wapanda Baiskeli kwa Kushiriki Barabara na Magari.",
    "With warmer weather approaching, more cyclists will be taking to the roads. To ensure everyone's safety, it's important for both cyclists and drivers to be aware of each other and follow basic safety rules. Here are some tips for cyclists:  * Wear a properly fitted helmet. * Use hand signals to indicate turns and stops. * Ride predictably and avoid weaving in and out of traffic. * Be aware of your surroundings and watch for car doors opening. * Obey traffic laws and stop signs.",
    "Huku hali ya hewa ya joto inapokaribia, waendesha baiskeli wengi zaidi watakuwa wakiingia barabarani. Ili kuhakikisha usalama wa kila mtu, ni muhimu kwa waendesha baiskeli na madereva kufahamu kila mmoja na kufuata sheria za msingi za usalama. Hapa kuna vidokezo kwa waendesha baiskeli: * Vaa vazi linalofaa. Chapeo chapeo kilichowekwa. * Tumia mawimbi ya mkono ili kuashiria zamu na vituo na uepuke kuingia na kutoka kwenye trafiki.",
    "18 May 2024",
    "18 Mei 2024"
));

// Article 3: Distracted Driving Dangers Highlighted in New Report
newsList.push(new NewsArticle(
    "Distracted Driving Dangers Highlighted in New Report by Road Safety Allies.",
    "Hatari za Uendeshaji zilizokengeushwa Zilizoangaziwa katika Ripoti Mpya ya Washirika wa Usalama Barabarani.",
    "A new report released by [Organization Name] highlights the dangers of distracted driving. The report found that distracted driving is a major factor in a significant number of road accidents. Common distractions include using cell phones, texting, eating, or adjusting in-car entertainment systems. The report urges drivers to put away distractions and focus solely on the road while driving.",
    "Ripoti mpya iliyotolewa na [Jina la Shirika] inaangazia hatari za kuendesha gari ovyo. Ripoti hiyo iligundua kuwa kuendesha gari kwa ovyo ovyo ni sababu kuu ya idadi kubwa ya ajali za barabarani. Vikengeusha-kevu vya kawaida ni pamoja na kutumia simu za mkononi, kutuma ujumbe mfupi, kula, au kurekebisha- mifumo ya burudani ya gari.",
    "17 May 2024",
    "17 Mei 2024"
));

// Article 4: School Bus Safety Reminders for Parents and Drivers
newsList.push(new NewsArticle(
    "School Bus Safety Reminders for Parents and Drivers as School Year Ends.",
    "Vikumbusho vya Usalama vya Mabasi ya Shule kwa Wazazi na Madereva Mwaka wa Shule unapoisha.",
    "With the school year coming to an end, it's important to remind everyone about school bus safety. Parents should ensure their children know the safety rules when waiting for and boarding the bus. Drivers must be extra cautious around school buses and obey stop signs when the bus is stopped with its red lights flashing. By working together, we can ensure a safe end to the school year for all students.",
    "Wakati mwaka wa shule unakaribia kumalizika, ni muhimu kukumbusha kila mtu kuhusu usalama wa mabasi ya shule. Wazazi wanapaswa kuhakikisha watoto wao wanajua sheria za usalama wakati wa kusubiri na kupanda basi. Madereva wanapaswa kuwa waangalifu zaidi karibu na mabasi ya shule na kutii ishara za kusimama wakati basi limesimamishwa huku taa zake nyekundu zikiwaka Kwa kufanya kazi pamoja, tunaweza kuhakikisha mwaka wa shule unaisha salama kwa wanafunzi wote.",
    "17 December 2023",
    "17 Disemba 2023"
));

// Article 5: Importance of Regular Vehicle Maintenance for Road Safety
newsList.push(new NewsArticle(
    "Don't Skip on Maintenance: Why Regular Vehicle Checks Are Important for Road Safety.",
    "Usiruke Matengenezo: Kwa Nini Ukaguzi wa Mara kwa Mara wa Magari ni Muhimu kwa Usalama Barabarani.",
    "Many drivers may not realize that a poorly maintained vehicle can be a major safety hazard. Regular maintenance checks are essential to ensure your vehicle is operating safely. This includes checking tire pressure and tread wear, replacing worn-out brake pads, and ensuring all lights and signals are functioning properly. By prioritizing vehicle maintenance, you can help prevent accidents and keep yourself and others safe on the road.",
    "Madereva wengi wanaweza wasitambue kuwa gari lisilotunzwa vizuri linaweza kuwa hatari kubwa kwa usalama. Kukagua matengenezo ya mara kwa mara ni muhimu ili kuhakikisha gari lako linafanya kazi kwa usalama. Hii ni pamoja na kuangalia shinikizo la tairi na uchakavu wa kukanyaga, kubadilisha breki zilizochakaa na kuhakikisha kila kitu kinatumika kwa usalama.",
    "16 May 2024",
    "16 Mei 2024"
));


const TheDate = document.getElementById("NewsDate");
const Title = document.getElementById("NewsTitle");
const STitle = document.getElementById("SNewsTitle");
const Content = document.getElementById("NewsContent");
const SContent = document.getElementById("SNewsContent");

let currentIndex = 0;

function updateHeadline() {
    var firstArticle = newsList[currentIndex];
    currentIndex = (currentIndex + 1) % newsList.length;
    TheDate.textContent = firstArticle.date;
    Title.textContent = firstArticle.title;
    STitle.textContent = firstArticle.stitle;
    Content.textContent = firstArticle.content;
    SContent.textContent = firstArticle.scontent;
}

setInterval(updateHeadline, 5000);