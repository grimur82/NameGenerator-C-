FROM mono:6.12

WORKDIR /app

COPY Driver.cs Markov.cs Parser.cs WebServer.cs test.txt ./
COPY web ./web

RUN mcs -out:NameGenerator.exe Driver.cs Markov.cs Parser.cs WebServer.cs

ENTRYPOINT ["mono", "NameGenerator.exe"]
