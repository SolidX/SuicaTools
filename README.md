# Suica Tools
A .NET application to decode history entries from [Suica](https://en.wikipedia.org/wiki/Suica) cards in to human readable information. This project was started half out of curiousity about how Japanese transit smart cards work and half out of a desire to try out .NET 6.

## Goals
- ~~Decode history entries written to IC cards~~
- Export entries as financial data files (probably OFX)
- A fancy UI maybe?

## How to Build
You're going to need Visual Studio 2022 and .NET 6.0 for this project.

## Data Sources
### Train Station Information
IC cards store the entry/exit train station on them as a triple of bytes representing the Region, Line, and Station. This encoding is known as the [サイバネコード](https://ja.wikipedia.org/wiki/%E9%A7%85%E3%82%B3%E3%83%BC%E3%83%89#%E3%82%B5%E3%82%A4%E3%83%90%E3%83%8D%E3%82%B3%E3%83%BC%E3%83%89) or Saibane Code and is maintained and defined by the [Japan Railway Engineers' Association](https://www.jrea.or.jp/). Not all codes are assigned to train stations, some are for testing purposes, specific shops in stations, or particular financial transactions.

In order to provide useful information, I've mapped many of these codes to their corresponding EkiData ids. [EkiData](https://ekidata.jp/) is a popular database cotnaining most Japanese railway stations. I then used a variety of sources (espeically Wikipedia) to localize the names of railway operators, lines, and some stations for English speakers. Huge thanks to @piuccio whose ["open-data-jp"](https://github.com/piuccio?utf8=%E2%9C%93&tab=repositories&q=open-data-jp-rail&type=&language=) projects made this a lot easier. Despite that this still was the longest part of the development process.

### Bus Stop Information
TBD

### Point of Sale Information
That's a very big  TBD