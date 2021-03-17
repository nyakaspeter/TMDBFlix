# TMDBFlix
TMDBFlix is a simple TMDb database browser for Universal Windows Platform with some torrent streaming capabilities.

## Features

* Browse and search between movies, tv shows and people
* View movie/show details, cast and crew information
* Watch trailers and production images in-app
* Get movie/show recommendations
* Search for torrents on popular trackers (requires Jackett API)
* Stream torrents (requires Node.js and Peerflix)
* Fluent design

## Screenshots
![2021-03-17 (2)](https://user-images.githubusercontent.com/43880678/111545798-e999e600-8776-11eb-98ed-95227ba1ea2b.png)
![2021-03-17 (3)](https://user-images.githubusercontent.com/43880678/111545800-eb63a980-8776-11eb-9183-61c6f7b959a9.png)
![2021-03-17 (4)](https://user-images.githubusercontent.com/43880678/111545803-ee5e9a00-8776-11eb-9a6a-4ab0a7aa54e1.png)
![2021-03-17 (5)](https://user-images.githubusercontent.com/43880678/111545807-ef8fc700-8776-11eb-8597-cb9a691c63a7.png)

## How to build

1. Download and install [Windows 10 SDK 10.0.17763](https://developer.microsoft.com/hu-hu/windows/downloads/sdk-archive/). Other SDK versions may or may not work.
2. You'll have to manually add references for these two files for the `TMDBFlix.Desktop` project:
```
C:\Program Files (x86)\Windows Kits\10\References\10.0.17763.0\Windows.Foundation.FoundationContract\3.0.0.0\Windows.Foundation.FoundationContract.winmd
C:\Program Files (x86)\Windows Kits\10\References\10.0.17763.0\Windows.Foundation.UniversalApiContract\7.0.0.0\Windows.Foundation.UniversalApiContract.winmd
```
3. Open `TMDBFlix.sln`, set `TMDBFlix.Packaging` as startup project, then build\deploy.

## How to stream torrents

1. Download and set up [Jackett](https://github.com/Jackett/Jackett)
2. Download and install [Node.js](https://nodejs.org/en/download/)
3. Install Peerflix globally from the command line:
```
npm install -g peerflix
```
4. In TMDBFlix configure Jackett and Peerflix from the settings screen
5. On a movie or show detail screen switch to the Torrents tab, select a torrent then press the play button
