# Laboratory-of-Inventions-RabbitMQ-Test-Task

���������� RabbitMQ � ������� ������������ � ���������� �������(appsetting.json)


������� ������ ���� ��� �������� ����� �� ��������� ����� xml ������.
��������� ����� ������ ��������� �����.(��� �������)
��������� ����	������ ��������� �����.(��� �������)
�������� �����������.  
������� ���� ����� ���� ��� �� ��� �������


(Background Service to check changed on folder)
� ������ ������� ��������� �����(�������� ��� ����� ���� �����) ��� ������ � ��������� �����.
����� ������� ������ ��� ���������� ������� ������ � ��������� ����������, ����� ����� ������ 
���� � ��������� � ��� ������ ���������� <?Xml...?>, ����� ���������� ���������� ����������-
������ ����� � ������ � ��������� � RabbiMQ. �����, ������� ��� ������� ������ ������� � ����-
������ � ��������� ��������� � ��, ����� �� ����� �������� � ���������� ������ �����.




StringBuilder contents = new StringBuilder(File.ReadAllText(FileParserService.UnreadXmlPath + "status.xml"));
contents.Replace("<?", "<!--<?").Replace("?>", "?>-->");
Console.WriteLine(contents.ToString());
XDocument doc = XDocument.Parse(contents.ToString());
InstrumentStatus a = SerializationUtil.Deserialize<InstrumentStatus>(doc);
return;



