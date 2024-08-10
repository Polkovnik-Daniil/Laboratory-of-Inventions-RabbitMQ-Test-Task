# Laboratory-of-Inventions-RabbitMQ-Test-Task

Развернуть RabbitMQ и указать пользователя в параметрах проекта(appsetting.json)


Сделать вечный цикл для проверки папки на появление новых xml файлов.
Проверять папку должен отдельный поток.(Как вариант)
Считывать файл	должен отдельный поток.(Как вариант)
Добавить логирование.  
Удалять файл после того как он был прочтен


(Background Service to check changed on folder)
В начале открыть отдельный поток(возможно это может быть таска) для чтения и запустить поток.
После запуска потока его необхожимо сделать вечным и поставить секундомер, после этого читаем 
файл и блокируем в нем лишние декларации <?Xml...?>, после блокировки необходимо десерелизи-
ровать стрим в объект и направить в RabbiMQ. После, обратно его принять другим потоком и обра-
ботать с внесением изменений в БД, также не стоит забывать о постоянном чтении файла.




StringBuilder contents = new StringBuilder(File.ReadAllText(FileParserService.UnreadXmlPath + "status.xml"));
contents.Replace("<?", "<!--<?").Replace("?>", "?>-->");
Console.WriteLine(contents.ToString());
XDocument doc = XDocument.Parse(contents.ToString());
InstrumentStatus a = SerializationUtil.Deserialize<InstrumentStatus>(doc);
return;



