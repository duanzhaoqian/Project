#Copyright 2008 by Roger Knapp, Licensed under the Apache License, Version 2.0

---------------------
BUILDING THE PROJECT:
---------------------

You simply need to build the project.  The project is Visual Studio 2008, the
.Net runtime 2.0 is being used.

You may need to modify the pre/post build events to locate the tools used:
"c:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\xsd.exe"
"c:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\sgen.exe"

------------------
USING THE PROJECT:
------------------

The prebuild event:
"c:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\xsd.exe" schema0.xsd /c /f
will generate schema0.cs, allow you to modify the xsd and see the code that is
generated.  inversly when you modify the classes in Log4net.cs the postbuild
event:
"c:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\xsd.exe" log4net.config.dll /outputdir:..\..\ /type:log4net
will rebuild the schema0.xsd to reflect the changes in the classes.

To use the library to read/write the configuration you need to add a reference
to log4net.config.dll and use the following code:

log4net config = log4net.Read( someTextReader );
...
config.Write( someTextWriter );
-or-
string xml = confix.ToXml();

-------------
AUTHORS NOTE:
-------------

In case your wondering why this does NOT reference log4net and instead recreates
the enumerations used, it's primarily due to the fact that we don't expose the
log4net assembly outside of our core logging interface which isolates us from
log4net.  Additionally there are some types that have been modified strictly for
the purpose of the xml serializer.