# Feliz Routing

This is an example of [Feliz Router](https://github.com/Zaid-Ajaj/Feliz.Router) in a [SAFE](https://safe-stack.github.io/) V4.1.1 app, which requires .NET 6 and node.js 16.

The template Todo app module has been duplicated and renamed as 'Page1' and 'Page2', and these pages have had their HTML header labels updated to reflect this.

The Index module now contains the logic needed to monitor for URL changes, parse the segments and use them to render a page.

Page 1 is the default so can be accessed at the root or `/page1`.

Page 2 can be found at `/page2` and any other route will show a Not Found page.

As usual, start the app by running `dotnet tool restore` (the first time only) and then `dotnet run` at the solution root.
