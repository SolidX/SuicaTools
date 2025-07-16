# Suica Tools
A .NET application to decode history entries from [Suica](https://en.wikipedia.org/wiki/Suica) cards in to human readable information. This project was started half out of curiousity about how Japanese transit smartcards work and half out of a desire to try out .NET 6.
Now I only work on it when my friends are in Japan and I'm stuck here.

## Goals
- ~~Decode history entries written to IC cards~~
- Localize Train Station Information
- ~~Export entries as financial data files~~ (QIF because what is the OFX format)
- A fancy UI maybe?

## How to Build
- Solidus.SuicaTools
	- You're going to need Visual Studio 2022 and .NET 6.0 for the Solidus.SuicaTools library.
	- Remember to update the appsettings.json file with the database connection string.
- Solidus.SuicaTools.CardReaderCLI
	- You'll need Windows 10 or higher and a FeliCa compatible NFC card reader to build and run the Solidus.SuicaTools.CardReaderCLI project.
	- For Sony card readers, you may need to install specific drivers first rather than relying on plug and play.
- TransitStations
	- This is a SQL Server database project. Deploy and populate it in order avoid errors when trying to transit-realated transactions.

## Card Reader Integration
At some point I decided I wanted to actually be able to read the information directly from transit cards while maintaining a pure C# codebase. This can generally be regarded as a mistake.
However, after some sporadic effort over the last year, I imported a Sony RC-S380 NFC card reader and found a seemingly incomplete [PC/SC SDK that uses the Windows Smartcard APIs](https://github.com/SolidX/PcscSdk).
After modernizing that a bit, tracking down the documentation for Sony's [FeliCa communication protocol](https://www.sony.co.jp/en/Products/felica/business/tech-support/st_usmnl.html), I banged out a CLI that reads IC cards and displays their transit history if they're valid Suica cards.

## Data Sources
### History Data Format
IC Card transaction history is stored as an array of 16 bytes and a card can hold up to 20 transaction records. The format of this data was reverse engineered by enthusiasts and is partially explained [here](http://jennychan.web.fc2.com/format/suica.html).

### Train Station Information
The entry & exit train stations are stored as a triple of bytes representing the Region, Line, and Station. This encoding is known as the [サイバネコード](https://ja.wikipedia.org/wiki/%E9%A7%85%E3%82%B3%E3%83%BC%E3%83%89#%E3%82%B5%E3%82%A4%E3%83%90%E3%83%8D%E3%82%B3%E3%83%BC%E3%83%89) or Saibane Code and is maintained and defined by the [Japan Railway Engineers' Association](https://www.jrea.or.jp/). However, not all codes are assigned to train stations. Some are for testing purposes, specific shops in stations, or particular financial transactions.

After scouring the internet, I found an ancient CSV dump of Saibane Code to station mappings reverse engineered by Japanese railfans. The file lacks headers and contains duplicates but is available [here](Data%20Dources/StationCode.csv).

In order to provide useful information, I've mapped many of these codes to their corresponding EkiData station ids. [EkiData](https://ekidata.jp/) is a popular database containing most Japanese railway stations. I then used a variety of sources (espeically Wikipedia) to localize the names of railway operators, lines, and some stations for English speakers. Huge thanks to @piuccio whose ["open-data-jp"](https://github.com/piuccio?utf8=%E2%9C%93&tab=repositories&q=open-data-jp-rail&type=&language=) projects made this a lot easier.

### Bus Stop Information
Bus stop information is stored as 2 16-bit unsigned integers, one for the Bus Line and one for the Bus Stop.

I've found a CSV dump of the [IruCa](https://en.wikipedia.org/wiki/IruCa) Bus Stop codes and included it [here](Data%20Dources/IruCaStationCode.csv).

### Point of Sale Information
That's a very big TBD
