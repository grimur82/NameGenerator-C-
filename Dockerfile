FROM mono:6.12

WORKDIR /app

COPY Driver.cs Markov.cs Parser.cs test.txt ./

RUN mcs -out:NameGenerator.exe Driver.cs Markov.cs Parser.cs

ENTRYPOINT ["mono", "NameGenerator.exe"]
