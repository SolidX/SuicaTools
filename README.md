# Suica Tools
A .NET application to decode history entries from [Suica](https://en.wikipedia.org/wiki/Suica) cards in to human readable information. This project was started half out of curiousity about how Japanese transit smart cards work and half out of a desire to try out .NET 6.

## Goals
- ~~Decode history entries written to IC cards~~
- Localize Train Station Information
- ~~Export entries as financial data files~~ (QIF because what is the OFX format)
- A fancy UI maybe?

## How to Build
You're going to need Visual Studio 2022 and .NET 6.0 for this project.

## Data Sources
### History Data Format
IC Card transaction history is stored as an array of 16 bytes and a card can hold up to 20 transaction records. The format of this data was reverse engineered by enthusiasts and is partially explained [here](http://jennychan.web.fc2.com/format/suica.html).

### Train Station Information
The entry & exit train stations are stored as a triple of bytes representing the Region, Line, and Station. This encoding is known as the [サイバネコード](https://ja.wikipedia.org/wiki/%E9%A7%85%E3%82%B3%E3%83%BC%E3%83%89#%E3%82%B5%E3%82%A4%E3%83%90%E3%83%8D%E3%82%B3%E3%83%BC%E3%83%89) or Saibane Code and is maintained and defined by the [Japan Railway Engineers' Association](https://www.jrea.or.jp/). However, not all codes are assigned to train stations. Some are for testing purposes, specific shops in stations, or particular financial transactions.

After scouring the internet, I found an ancient CSV dump Saibane Code to station mappings reverse engineered by Japanese railfans. The file lacks headers and contains duplicates but is available [here](Data%20Dources/StationCode.csv).

In order to provide useful information, I've mapped many of these codes to their corresponding EkiData station ids. [EkiData](https://ekidata.jp/) is a popular database containing most Japanese railway stations. I then used a variety of sources (espeically Wikipedia) to localize the names of railway operators, lines, and some stations for English speakers. Huge thanks to @piuccio whose ["open-data-jp"](https://github.com/piuccio?utf8=%E2%9C%93&tab=repositories&q=open-data-jp-rail&type=&language=) projects made this a lot easier.

### Bus Stop Information
Bus stop information is stored as 2 16-bit unsigned integers, one for the Bus Line and one for the Bus Stop.

I've found a CSV dump of the [IruCa](https://en.wikipedia.org/wiki/IruCa) Bus Stop codes and included it [here](Data%20Dources/IruCaStationCode.csv).

### Point of Sale Information
That's a very big TBD