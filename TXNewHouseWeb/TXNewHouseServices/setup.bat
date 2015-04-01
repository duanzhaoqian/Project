
cd /d c:\Windows\Microsoft.NET\Framework\v4.0.30319
installutil.exe D:\Office\Develop\kyjPrj\pc\xinfang\1.0\TXNewHouseServices\bin\Debug\TXNewHouseServices.exe
Net Start ServiceNewHouse
sc config ServiceNewHouse start= auto