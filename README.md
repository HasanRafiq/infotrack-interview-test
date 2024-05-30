# InfoTrack Web Scraper Interview Test Implementation - Hasan Rafiq

-   Visual Studio 2022 used for .NET programming
-   Visual Studio Code used for Typescript programming

## Getting Started

-   Backend:

    -   Run dotnet restore to restore project dependencies.
    -   Select InfoTrackWebScraper.API as startup project.
    -   Run API (or dotnet run)

-   Frontend:

    -   Navigate to InfotrackWebScraper.Frontend
    -   run npm install
    -   run npm start
    -   open http://localhost:3000 (if it didn't automatically)

Now type your url and keyword combo

### Known bugs:

-   Tested and working with example url and keyword

    -   (https://www.google.co.uk/search?num=100&q=land+registry+search, www.infotrack.co.uk) (and many other keywords)

-   If error found for valid url, reduce num query param in google search url. I think this is because of an issue when parsing into an XmlDocument type.

-   Example (may vary based on SEO):

    -   Working correctly (https://www.google.co.uk/search?num=3&q=kfc, chicken)
    -   Failing (https://www.google.co.uk/search?num=4&q=kfc, chicken)
