# SolarToTelegrafHTTPProxy
[![master](https://github.com/owenashurst/SolarToTelegrafHTTPProxy/actions/workflows/dotnet.yml/badge.svg)](https://github.com/owenashurst/SolarToTelegrafHTTPProxy/actions)

This API parses the data from an Iconica WiFi monitoring kit and proxies the request to Telegraf over HTTP as JSON.
This allows anyone to produce their own Solar monitoring dashboard using something such as Grafana.

## Requirements
This relies on the Telegraf [http_listener_v2](https://github.com/influxdata/telegraf/tree/master/plugins/inputs/http_listener_v2) being enabled in the telegraf config.

### Linux
In order to send data to Telegraf, you will need to check your `telegraf.conf` and match the following values listed below.

Path: `/etc/telegraf/telegraf.conf`

```
...
# # Generic HTTP write listener
[[inputs.http_listener_v2]]
# the name of the measurement
name_override = "solar"

#   ## Address and port to host HTTP listener on
service_address = ":4000"
#
#   ## Path to listen to.
path = "/telegraf"
#
#   ## HTTP methods to accept.
methods = ["POST"]

#
#   ## maximum duration before timing out read of the request
#   # read_timeout = "10s"
#   ## maximum duration before timing out write of the response
#   # write_timeout = "10s"
#
#   ## Maximum allowed http request body size in bytes.
#   ## 0 means to use the default of 524,288,00 bytes (500 mebibytes)
#   # max_body_size = "500MB"
#
#   ## Part of the request to consume.  Available options are "body" and
#   ## "query".
   data_source = "body"
   
   ...
   
#   ## Data format to consume.
#   ## Each data format has its own unique set of configuration options, read
#   ## more about them here:
#   ## https://github.com/influxdata/telegraf/blob/master/docs/DATA_FORMATS_INPUT.md
   data_format = "json"
...
```

## Setup
In appsettings.json, you can change the API URL and port that you've configured the telegraf [http_listener_v2](https://github.com/influxdata/telegraf/tree/master/plugins/inputs/http_listener_v2) to use.

## Docker
This project comes with a ready to use `Dockerfile` file. I would suggest cloning the code into a sub-folder called `app` and create
a `docker-compose.yml` file where you can inject your own environment variables to override settings such as
the API URL, etc.

### Example docker-compose.yml

```dockerfile
version: '3'

services:
  app:
    build: ./app
    container_name: SolarToTelegrafHTTPProxy
    ports:
      - 4001:80
    environment:
      - TelegrafSettings__HttpListenerApiURL=http://192.168.1.100:4000
    restart: unless-stopped
```

Given Telegraf is listening on port 4000, this Docker container will expose the API on port 4001.

## Nginx
If you wanted to create a reverse-proxy using Nginx so the request is directed to your docker container
(useful for if you have this hosted on an external server), then you can use the following `sites-available` config:

`/etc/nginx/sites-available/solar-monitoring.your-domain.com`

```
server {
        server_name solar-monitoring.your-domain.com;
        location /  {
                proxy_pass http://127.0.0.1:4001;
                proxy_set_header Host $host;
                proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
                proxy_set_header X-Real-IP $remote_addr;
                proxy_set_header X-Forwarded-Proto $scheme;
        }
}
```

Change the `server_name` to your own DNS.

Once done, simply run the command to activate the config;

```
ln -s /etc/nginx/sites-available/solar-monitoring.your-domain.com /etc/nginx/sites-enabled/solar-monitoring.your-domain.com
```

Then reload the Nginx web server:

`sudo service nginx restart`