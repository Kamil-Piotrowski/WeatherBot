# WeatherBot

### PL:
Pogodynkobot (Pogodobot?)<br/>
Przedstawia się i mówi, jaka jest pogoda w danym miejscu.<br/>
Wykorzystuje:<br/>
+ [MS Bot Framework](https://dev.botframework.com/)
+ [AccuWeather Api](https://developer.accuweather.com/)

Generalnie angielskojęzyczny (jak Api AccuWeather).
Nazwy niektórych polskich miast są tłumaczone an angielski - spis w pliku locations.json.
Przykład:
"Jak pogoda w białymstoku?" -> Bot wyłapie słowo "białymstoku" -> znajdzie taki klucz w pliku locations.json i pobierze wartość dla tego klucza ("Bialystok").
Ze słowem "Bialystok" AccuWeather już powinno sobie poradzić.

<br/><br/><br/>
### EN:
Weatherbot<br/>
Introduces itself and tells what's the weather in specified place.<br/>
Makes use of:<br/>
+ [MS Bot Framework](https://dev.botframework.com/)
+ [AccuWeather Api](https://developer.accuweather.com/)
