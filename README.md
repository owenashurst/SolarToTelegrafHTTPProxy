# SolarToTelegrafHTTPProxy
[![master](https://github.com/owenashurst/SolarToTelegrafHTTPProxy/actions/workflows/dotnet.yml/badge.svg)](https://github.com/owenashurst/SolarToTelegrafHTTPProxy/actions)

Parses the data from an Iconica WiFi monitoring kit and proxies the request to Telegraf over HTTP as JSON.

## Requirements
This relies on the Telegraf [http_listener_v2](https://github.com/influxdata/telegraf/tree/master/plugins/inputs/http_listener_v2) being enabled in the telegraf config.

## Setup
In appsettings.json, you can change the API URL and port that you've configured the telegraf [http_listener_v2](https://github.com/influxdata/telegraf/tree/master/plugins/inputs/http_listener_v2) to use.
