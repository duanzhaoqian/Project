#region Copyright 2008 by Roger Knapp, Licensed under the Apache License, Version 2.0
/* Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
#endregion
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;
using System.IO;

#region After building replace the existing <xs:element name="log4net" .../> with this:
/*
  <!-- Copyright 2008 by Roger Knapp, Licensed under the Apache License, Version 2.0 -->
  <xs:annotation>
    <xs:documentation>
      <![CDATA[
Author: Roger Knapp
Url:    http://csharptest.net/downloads/schema/log4net.xsd
Date:   November 7th, 2008
Rev:    1.01
Usage:  You only need to add the following to the log4net element:
        <log4net 
          xsi:noNamespaceSchemaLocation="http://csharptest.net/downloads/schema/log4net.xsd" 
          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

Preface:
Please be aware that though this xsd attempts to cover most of the usages you 
will encounter with log4net configurations it is by no means complete.  The 
primary reason (excuse) for this is that the xml format used by log4net config 
files does not conform to any real standard.  This is both it's primary 
weakness and it's strength.  As such there are a few things you should know
about how log4net interprets your configuration before creating one.

Firstly most of the elements described in this document are really not needed.  
This is due to the fact that a <param name='x' ...> can be used in place of
the element <x ...>.  For instance, the following are equivalent:
    <maximumFileSize value="1MB" />
    <param name="maximumFileSize" value="1MB" />

So Why Use This Xsd?:
This xsd attempts to utilize the former convention and to declare as much as
possible for the benefit of auto-completion and sanity checking.  However, 
since most of the param names vary by the containing element's 'type' attribute 
it is impossible to describe this correctly in xsd (at least to my knowledge).  
So use this schema if you like and remember that there is life beyond it's 
limited capability.

If you would like to contribute back to this schema you can send email to the
follow address (remove all the spaces and slashes): schema / @ / csharptest.net
Be sure to include the schema name "log4net.xsd" in the subject-line.  If you
know someone on the log4net project please convince them to adopt a schema,
I don't care which one, just have one.

Legal:
   Copyright 2008 by Roger Knapp

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
]]>
    </xs:documentation>
    <xs:appinfo source="http://logging.apache.org/log4net/release/manual/configuration.html" />
  </xs:annotation>
  <xs:element name="log4net" type="log4net">
    <xs:key name="appenderNamesKey">
      <xs:selector xpath="./appender" />
      <xs:field xpath="@name" />
    </xs:key>
    <xs:keyref name="appenderExists" refer="appenderNamesKey">
      <xs:selector xpath=".//appender-ref"/>
      <xs:field xpath="@ref"/>
    </xs:keyref>
  </xs:element>
*/
#endregion

[XmlRoot]
[Serializable]
public class log4net
{
	static readonly XmlSerializer _serializer = new XmlSerializer( typeof( log4net ) );
	public static log4net Read( System.IO.TextReader rdr ) { return (log4net)_serializer.Deserialize( rdr ); }
	public void Write( System.IO.TextWriter wtr ) { _serializer.Serialize( wtr, this ); }
	public string ToXml() { StringWriter sw = new StringWriter(); Write( sw ); return sw.ToString(); }

	/// <summary>
	/// Optional attribute. Value must be either true or false. The default value is false. 
	/// Set this attribute to true to enable internal log4net debugging for this configuration.  
	/// </summary>
	[XmlAttribute][DefaultValueAttribute(false)]
	public bool debug;

	/// <summary>
	/// Optional attribute. Value must be either Merge or Overwrite. The default value is Merge. 
	/// Set this attribute to Overwrite to reset the configuration of the repository being 
	/// configured before applying this configuration.  
	/// </summary>
	[XmlAttribute]
	[DefaultValueAttribute( log4netUpdateAttribute.Merge )]
	public log4netUpdateAttribute update;
		
	/// <summary>
	/// Optional attribute. Value must be the name of a level registered on the repository. 
	/// The default value is ALL. Set this attribute to limit the messages that are logged across 
	/// the whole repository, regardless of the logger that the message is logged to.  
	/// </summary>
	[XmlAttribute]
	[DefaultValueAttribute( log4netLevel.ALL )]
	public log4netLevel threshold;

	[XmlElement( typeof( root ) )]
	[XmlElement( typeof( appender ) )]
	[XmlElement( typeof( logger ) )]
	[XmlElement( typeof( renderer ) )]
	[XmlElement( typeof( param ) )]
	public object[] children;
}

/// <summary>
/// appender Zero or more elements allowed. Defines an appender.  
/// </summary>
public class appender
{
	[XmlAttribute]
	public string name;
	[XmlAttribute]
	public log4netAppenderTypes type;

	[XmlElement( "appender-ref", typeof( appender_ref ) )]
	[XmlElement( typeof( filter ) )]
	[XmlElement( typeof( layout ) )]
	[XmlElement( typeof( param ) )]
	//common
	[XmlElement( typeof( securityContext ) )]
	[XmlElement( typeof( bufferSize ) )]
	[XmlElement( typeof( threshold ) )]
	[XmlElement( typeof( lossy ) )]
	[XmlElement( typeof( evaluator ) )]
	//AdoNetAppender
	[XmlElement( typeof( commandText ) )]
	[XmlElement( typeof( commandType ) )]
	[XmlElement( typeof( connectionString ) )]
	[XmlElement( typeof( connectionType ) )]
	[XmlElement( typeof( parameter ) )]
	[XmlElement( typeof( reconnectOnError ) )]
	[XmlElement( typeof( useTransactions ) )]
	//AnsiColorTerminalAppender, ColoredConsoleAppender, and ConsoleAppender
	[XmlElement( typeof( target ) )]
	//AnsiColorTerminalAppender, ColoredConsoleAppender, EventLogAppender, LocalSyslogAppender, RemoteSyslogAppender
	[XmlElement( typeof( mapping ) )]
	//TextWriterAppender
	[XmlElement( typeof( immediateFlush ) )]
	//EventLogAppender
	[XmlElement( typeof( applicationName ) )]
	[XmlElement( typeof( logName ) )]
	[XmlElement( typeof( machineName ) )]
	//FileAppender
	[XmlElement( typeof( appendToFile ) )]
	[XmlElement( typeof( encoding ) )]
	[XmlElement( typeof( file ) )]
	[XmlElement( typeof( lockingModel ) )]
	//LocalSyslogAppender
	[XmlElement( typeof( facility ) )]
	[XmlElement( typeof( identity ) )]
	//NetSendAppender
	[XmlElement( typeof( recipient ) )]
	[XmlElement( typeof( sender ) )]
	[XmlElement( typeof( server ) )]
	//RemotingAppender
	[XmlElement( typeof( sink ) )]
	//RollingFileAppender
	[XmlElement( typeof( countDirection ) )]
	[XmlElement( typeof( datePattern ) )]
	[XmlElement( typeof( maxFileSize ) )]
	[XmlElement( typeof( maximumFileSize ) )]
	[XmlElement( typeof( maxSizeRollBackups ) )]
	[XmlElement( typeof( rollingStyle ) )]
	[XmlElement( typeof( staticLogFileName ) )]
	//SmtpAppender & SmtpPickupDirAppender
	[XmlElement( typeof( from ) )]
	[XmlElement( typeof( priority ) )]
	[XmlElement( typeof( subject ) )]
	[XmlElement( typeof( to ) )]
	//SmtpAppender
	[XmlElement( typeof( authentication ) )]
	[XmlElement( typeof( password ) )]
	[XmlElement( typeof( smtpHost ) )]
	[XmlElement( "username", typeof( userName ) )]
	//SmtpAppender & TelnetAdapter
	[XmlElement( typeof( port ) )]
	//SmtpPickupDirAppender
	[XmlElement( typeof( pickupDir ) )]
	//UdpAppender
	[XmlElement( typeof( localPort ) )]
	[XmlElement( typeof( remoteAddress ) )]
	[XmlElement( typeof( remotePort ) )]
	public object[] children;
}

/// <summary>
/// root Optional element, maximum of one allowed. Defines the configuration of the root logger.  
/// </summary>
public class root
{
	[XmlElement( "appender-ref", typeof( appender_ref ) )]
	[XmlElement( typeof( level ) )]
	[XmlElement( typeof( param ) )]
	public object[] children;
}

/// <summary>
/// logger Zero or more elements allowed. Defines the configuration of a logger. 
/// </summary>
public class logger
{
	[XmlAttribute]
	public string name;
	/// <summary>
	/// Value may be either true or false. The default value is true. Set this attribute 
	/// to false to prevent this logger from inheriting the appenders defined on parent loggers. 
	/// </summary>
	[XmlAttribute]
	[DefaultValueAttribute( true )]
	public bool additivity;

	[XmlElement( "appender-ref", typeof( appender_ref ) )]
	[XmlElement( typeof( level ) )]
	[XmlElement( typeof( param ) )]
	public object[] children;
}

/// <summary>
/// renderer Zero or more elements allowed. Defines an object renderer.  
/// </summary>
public class renderer
{
	[XmlAttribute]
	public string renderingClass;

	[XmlAttribute]
	public string renderedClass;
}

/// <summary>
/// param Zero or more elements allowed. Repository specific parameters  
/// </summary>
public class param
{
	[XmlAttribute]
	public string name;
	
	[XmlAttribute]
	[DefaultValue( null )]
	public string value;

	[XmlAttribute]
	[DefaultValue( null )]
	public string type;
	
	[XmlElement("param")]
	public param[] parameters;
}

/// <summary>
/// Optional element, maximum of one allowed. Defines the logging level for this logger. 
/// This logger will only accept event that are at this level or above. 
/// </summary>
public class level
{
	[XmlAttribute]
	public log4netLevel value;
}

/// <summary>
/// Zero or more elements allowed. Defines the filters used by this appender. 
/// </summary>
public class filter
{
	[XmlAttribute]
	public log4netFilterTypes type;

	[XmlElement( typeof( param ) )]
	[XmlElement( typeof( acceptOnMatch ) )]
	[XmlElement( typeof( levelToMatch ) )]
	[XmlElement( typeof( levelMin ) )]
	[XmlElement( typeof( levelMax ) )]
	[XmlElement( typeof( loggerToMatch ) )]
	[XmlElement( typeof( key ) )]
	[XmlElement( typeof( stringToMatch ) )]
	[XmlElement( typeof( regexToMatch ) )]
	public object[] parameters;
}

/// <summary>
/// Zero or more elements allowed. Defines the filters used by this appender. 
/// </summary>
public class layout
{
	[XmlAttribute]
	public log4netLayoutTypes type;
	[XmlAttribute][DefaultValue(null)]
	public string value;

	[XmlElement( typeof( param ) )]
	[XmlElement( typeof( header ) )]
	[XmlElement( typeof( footer ) )]
	[XmlElement( typeof( ignoresException ) )]
	[XmlElement( typeof( conversionPattern ) )]
	[XmlElement( typeof( key ) )]
	[XmlElement( typeof( base64EncodeMessage ) )]
	[XmlElement( typeof( base64EncodeProperties ) )]
	public object[] parameters;
}

/// <summary>
/// Zero or more elements allowed. Allows the appender to reference other appenders. 
/// Not supported by all appenders. 
/// </summary>
public class appender_ref
{
	[XmlAttribute("ref")]
	public string name;
}

public enum log4netUpdateAttribute
{ Merge, Overwrite }

public enum log4netLevel
{ ALL, DEBUG, INFO, WARN, ERROR, FATAL, OFF }

#region Shared properties...

/// <summary>
/// Used by the RawPropertyLayout and the PropertyFilter
/// </summary>
public class key { [XmlAttribute] public string value; }

#endregion
#region Appenders...

public enum log4netAppenderTypes
{
	[XmlEnum( "log4net.Appender.AdoNetAppender" )]
	AdoNetAppender,
	[XmlEnum( "log4net.Appender.AnsiColorTerminalAppender" )]
	AnsiColorTerminalAppender,
	[XmlEnum( "log4net.Appender.AspNetTraceAppender" )]
	AspNetTraceAppender,
	[XmlEnum( "log4net.Appender.BufferingForwardingAppender" )]
	BufferingForwardingAppender,
	[XmlEnum( "log4net.Appender.ColoredConsoleAppender" )]
	ColoredConsoleAppender,
	[XmlEnum( "log4net.Appender.ConsoleAppender" )]
	ConsoleAppender,
	[XmlEnum( "log4net.Appender.DebugAppender" )]
	DebugAppender,
	[XmlEnum( "log4net.Appender.EventLogAppender" )]
	EventLogAppender,
	[XmlEnum( "log4net.Appender.FileAppender" )]
	FileAppender,
	[XmlEnum( "log4net.Appender.ForwardingAppender" )]
	ForwardingAppender,
	[XmlEnum( "log4net.Appender.LocalSyslogAppender" )]
	LocalSyslogAppender,
	[XmlEnum( "log4net.Appender.MemoryAppender" )]
	MemoryAppender,
	[XmlEnum( "log4net.Appender.NetSendAppender" )]
	NetSendAppender,
	[XmlEnum( "log4net.Appender.OutputDebugStringAppender" )]
	OutputDebugStringAppender,
	[XmlEnum( "log4net.Appender.RemoteSyslogAppender" )]
	RemoteSyslogAppender,
	[XmlEnum( "log4net.Appender.RemotingAppender" )]
	RemotingAppender,
	[XmlEnum( "log4net.Appender.RollingFileAppender" )]
	RollingFileAppender,
	[XmlEnum( "log4net.Appender.SmtpAppender" )]
	SmtpAppender,
	[XmlEnum( "log4net.Appender.SmtpPickupDirAppender" )]
	SmtpPickupDirAppender,
	[XmlEnum( "log4net.Appender.TelnetAppender" )]
	TelnetAppender,
	[XmlEnum( "log4net.Appender.TextWriterAppender" )]
	TextWriterAppender,
	[XmlEnum( "log4net.Appender.TraceAppender" )]
	TraceAppender,
	[XmlEnum( "log4net.Appender.UdpAppender" )]
	UdpAppender,
}

#region Common
/// <summary>
/// (inherited from BufferingAppenderSkeleton) Gets or sets the size of the cyclic 
/// buffer used to hold the logging events.  
/// </summary>
public class bufferSize { [XmlAttribute] public int value; }
/// <summary>
/// Threshold (inherited from AppenderSkeleton) Gets or sets the threshold Level of 
/// this appender.  
/// </summary>
public class threshold { [XmlAttribute] public log4netLevel value; }
/// <summary>
/// Lossy (inherited from BufferingAppenderSkeleton) Gets or sets a value that indicates 
/// whether the appender is lossy.  
/// </summary>
public class lossy { [XmlAttribute] public bool value; }
/// <summary>
/// Lossy (inherited from BufferingAppenderSkeleton) Gets or sets a value that indicates 
/// whether the appender is lossy.  
/// </summary>
public class evaluator 
{ 
	[XmlAttribute]
	public evaluatorTypes type;
	[XmlElement]
	public threshold threshold;
}
public enum evaluatorTypes { [XmlEnum( "log4net.Core.LevelEvaluator" )] LevelEvaluator }
/// <summary>
/// Shared by all Console appenders: AnsiColorTerminalAppender, ColoredConsoleAppender, 
/// and ConsoleAppender
/// </summary>
public class target { [XmlAttribute] public consoleTargetTypes value; }
public enum consoleTargetTypes { [XmlEnum( "Console.Out" )] Out, [XmlEnum( "Console.Error" )] Error }

/// <summary>
/// ImmediateFlush Gets or sets a value that indicates whether the appender 
/// will flush at the end of each write. 
/// Used by: DebugAppender, FileAppender
/// </summary>
public class immediateFlush { [XmlAttribute] public bool value; }
#endregion
#region Level Mappers
public class mapping
{
	[XmlElement( typeof( level ) )]
	[XmlElement( typeof( attributes ) )]
	[XmlElement( typeof( backColor ) )]
	[XmlElement( typeof( foreColor ) )]
	[XmlElement( typeof( eventLogEntryType ) )]
	[XmlElement( typeof( severity ) )]
	public object[] children;
}
	 
/// <summary>
/// AnsiColorTerminalAppender.Attributes The color attributes for the specified level  
/// </summary>
public class attributes { [XmlAttribute] public attributesTypes value; }
public enum attributesTypes { Bright, Dim, Underscore, Blink, Reverse, Hidden, Strikethrough }

/// <summary>
/// BackColor The mapped background color for the specified level  
/// </summary>
public class backColor { [XmlAttribute] public allColorTypes value; }
public enum allColorTypes { Black, Blue, Cyan, Green, HighIntensity, Magenta, Purple, Red, White, Yellow }	 
/// <summary>
/// ForeColor The mapped foreground color for the specified level  
/// </summary>
public class foreColor { [XmlAttribute] public allColorTypes value; }

/// <summary>
/// EventLogAppender.EventLogEntryType The EventLogEntryType for this entry  
/// </summary>
public class eventLogEntryType { [XmlAttribute] public System.Diagnostics.EventLogEntryType value; }

/// <summary>
/// *SyslogAppender.Severity The mapped syslog severity for the specified level 
/// </summary>
public class severity { [XmlAttribute] public severityTypes value; }
public enum severityTypes { Emergency,Alert,Critical,Error,Warning,Notice,Informational,Debug}

#endregion
#region AdoNetAppender
/// <summary>
/// AdoNetAppender.CommandText Gets or sets the command text that is used to insert 
/// logging events into the database.  
/// </summary>
public class commandText { [XmlAttribute] public string value; }
/// <summary>
/// AdoNetAppender.CommandType Gets or sets the command type to execute.  
/// </summary>
public class commandType { [XmlAttribute] public System.Data.CommandType value; }
/// <summary>
/// AdoNetAppender.ConnectionString Gets or sets the database connection string that 
/// is used to connect to the database.  
/// </summary>
public class connectionString { [XmlAttribute] public string value; }
/// <summary>
/// AdoNetAppender.ConnectionType Gets or sets the type name of the IDbConnection 
/// connection that should be created.  
/// </summary>
public class connectionType { [XmlAttribute] public connectionTypeTypes value; }
/// <summary>
/// AdoNetAppender.ReconnectOnError Should this appender try to reconnect to the 
/// database on error.  
/// </summary>
public class reconnectOnError { [XmlAttribute] public bool value; }
/// <summary>
/// AdoNetAppender.UseTransactions Should transactions be used to insert logging 
/// events in the database.  
/// </summary>
public class useTransactions { [XmlAttribute] public bool value; }

public enum connectionTypeTypes
{
	[XmlEnum("System.Data.OleDb.OleDbConnection, System.Data")]
	OleDbConnection,
	[XmlEnum("System.Data.SqlClient.SqlConnection, System.Data")]
	SqlConnection,
	[XmlEnum("Microsoft.Data.Odbc.OdbcConnection, Microsoft.Data.Odbc")]
	OdbcConnection,
	[XmlEnum( "System.Data.OracleClient.OracleConnection, System.Data.OracleClient" )]
	OracleConnection,
	[XmlEnum( "MySql.Data.MySqlClient.MySqlConnection, MySql.Data" )]
	MySqlConnection,
}
#endregion
#region EventLogAppender
/// <summary>
/// EventLogAppender.ApplicationName Property used to set the Application name. This appears in the event 
/// logs when logging.
/// </summary>
public class applicationName { [XmlAttribute] public string value; }

/// <summary>
/// EventLogAppender.LogName The name of the log where messages will be stored.  Defaults to 'Application'
/// </summary>
public class logName { [XmlAttribute] public string value; }

/// <summary>
/// EventLogAppender.MachineName This property is used to return the name of the computer to use when accessing 
/// the event logs. Currently, this is the current computer, denoted by a dot "." 
/// </summary>
public class machineName { [XmlAttribute] public string value; }
#endregion
#region FileAppender
/// <summary>
/// FileAppender.AppendToFile Gets or sets a flag that indicates whether the file should 
/// be appended to or overwritten.  
/// </summary>
public class appendToFile { [XmlAttribute] public bool value; }

/// <summary>
/// FileAppender.Encoding Gets or sets Encoding used to write to the file.  
/// </summary>
public class encoding { [XmlAttribute] public encodingTypes value; }
public enum encodingTypes
{
	[XmlEnum( "us-ascii" )]
	us_ascii,
	[XmlEnum( "utf-7" )]
	utf_7,
	[XmlEnum( "utf-8" )]
	utf_8,
	[XmlEnum( "utf-16" )]
	utf_16,
	[XmlEnum( "unicodeFFFE" )]
	unicodeFFFE,
	[XmlEnum( "utf-32" )]
	utf_32,
	[XmlEnum( "utf-32BE" )]
	utf_32BE,
}

/// <summary>
/// FileAppender.File Gets or sets the path to the file that logging will be written to.  
/// </summary>
public class file 
{
	[XmlAttribute] 
	public string value;
	[XmlAttribute]
	[DefaultValue( null )]
	public string type; 
}

/// <summary>
/// FileAppender.LockingModel Gets or sets the LockingModel used to handle locking of 
/// the file.  
/// </summary>
public class lockingModel { [XmlAttribute] public lockingModelTypes type; }
public enum lockingModelTypes { [XmlEnum( "log4net.Appender.FileAppender+ExclusiveLock" )] ExclusiveLock, [XmlEnum( "log4net.Appender.FileAppender+MinimalLock" )] MinimalLock }
#endregion
#region LocalSyslogAppender

/// <summary>
/// LocalSyslogAppender.Facility Syslog facility  
/// </summary>
public class facility { [XmlAttribute] public facilityTypes value; }
public enum facilityTypes
{
	Kernel = 0,
	User = 1,
	Mail = 2,
	Daemons = 3,
	Authorization = 4,
	Syslog = 5,
	Printer = 6,
	News = 7,
	Uucp = 8,
	Clock = 9,
	Authorization2 = 10,
	Ftp = 11,
	Ntp = 12,
	Audit = 13,
	Alert = 14,
	Clock2 = 15,
	Local0 = 16,
	Local1 = 17,
	Local2 = 18,
	Local3 = 19,
	Local4 = 20,
	Local5 = 21,
	Local6 = 22,
	Local7 = 23,
}
/// <summary>
/// LocalSyslogAppender.Identity Message identity  
/// </summary>
public class identity { [XmlAttribute] public string value; }

#endregion
#region NetSendAppender

/// <summary>
/// NetSendAppender.Recipient Gets or sets the message alias to which the message should be sent.  
/// </summary>
public class recipient { [XmlAttribute] public string value; }

/// <summary>
/// NetSendAppender.Sender Gets or sets the sender of the message.  
/// </summary>
public class sender { [XmlAttribute] public string value; }

/// <summary>
/// NetSendAppender.Server Gets or sets the DNS or NetBIOS name of the remote server on which the 
/// function is to execute. 
/// </summary>
public class server { [XmlAttribute] public string value; }

#endregion
#region RemotingAppender

/// <summary>
/// RemotingAppender.Sink Gets or sets the URL of the well-known object that will 
/// accept the logging events.  
/// </summary>
public class sink { [XmlAttribute( DataType = "anyURI" )] public string value; }

#endregion
#region RollingFileAppender
/// <summary>
/// RollingFileAppender.CountDirection Gets or sets the rolling file count direction.  
/// </summary>
public class countDirection { [XmlAttribute] public countDirectionTypes value; }
public enum countDirectionTypes { [XmlEnum( "-1" )] NewerLower, [XmlEnum( "1" )] NewerHigher }

/// <summary>
/// RollingFileAppender.DatePattern Gets or sets the date pattern to be used for 
/// generating file names when rolling over on date.  
/// </summary>
public class datePattern { [XmlAttribute] public string value; }

/// <summary>
/// RollingFileAppender.MaxFileSize Gets or sets the maximum size that the output 
/// file is allowed to reach before being rolled over to backup files.  
/// </summary>
public class maxFileSize { [XmlAttribute] public long value; }

/// <summary>
/// RollingFileAppender.MaximumFileSize Gets or sets the maximum size that the output 
/// file is allowed to reach before being rolled over to backup files.  
/// </summary>
public class maximumFileSize { [XmlAttribute] public string value; }

/// <summary>
/// RollingFileAppender.MaxSizeRollBackups Gets or sets the maximum number of backup 
/// files that are kept before the oldest is erased.  
/// </summary>
public class maxSizeRollBackups { [XmlAttribute] public int value; }

/// <summary>
/// RollingFileAppender.RollingStyle Gets or sets the rolling style.  
/// </summary>
public class rollingStyle { [XmlAttribute] public rollingStyleTypes value; }
public enum rollingStyleTypes { Once, Size, Date, Composite }

/// <summary>
/// RollingFileAppender.StaticLogFileName Gets or sets a value indicating whether to 
/// always log to the same file.  
/// </summary>
public class staticLogFileName { [XmlAttribute] public bool value; }

#endregion
#region SmtpAppender

/// <summary>
/// Authentication The mode to use to authentication with the SMTP server  
/// </summary>
public class authentication { [XmlAttribute] public authenticationTypes value; }
public enum authenticationTypes { None, Basic, Ntlm }

/// <summary>
/// From Gets or sets the e-mail address of the sender.  
/// </summary>
public class from { [XmlAttribute] public string value; }

/// <summary>
/// Port The port on which the SMTP server is listening  
/// </summary>
public class port { [XmlAttribute] public int value; }

/// <summary>
/// Priority Gets or sets the priority of the e-mail message  
/// </summary>
public class priority { [XmlAttribute] public priorityTypes value; }
public enum priorityTypes { Normal, Low, High }

/// <summary>
/// SmtpHost Gets or sets the name of the SMTP relay mail server to use to send the e-mail messages.  
/// </summary>
public class smtpHost { [XmlAttribute] public string value; }

/// <summary>
/// Subject Gets or sets the subject line of the e-mail message.  
/// </summary>
public class subject { [XmlAttribute] public string value; }

/// <summary>
/// To Gets or sets a semicolon-delimited list of recipient e-mail addresses.  
/// </summary>
public class to { [XmlAttribute] public string value; }

#endregion
#region SmtpPickupDirAppender

/// <summary>
/// SmtpPickupDirAppender.PickupDir Gets or sets the path to write the messages to. 
/// </summary>
public class pickupDir { [XmlAttribute] public string value; }

#endregion
#region UdpAppender

/// <summary>
/// UdpAppender.LocalPort Gets or sets the TCP port number from which the 
/// underlying UdpClient will communicate.  
/// </summary>
public class localPort { [XmlAttribute] public int value; }

/// <summary>
/// UdpAppender.RemoteAddress Gets or sets the IP address of the remote 
/// host or multicast group to which the underlying UdpClient should sent the 
/// logging event.  
/// </summary>
public class remoteAddress { [XmlAttribute] public string value; }

/// <summary>
/// UdpAppender.RemotePort Gets or sets the TCP port number of the remote 
/// host or multicast group to which the underlying UdpClient should sent the 
/// logging event.  
/// </summary>
public class remotePort { [XmlAttribute] public int value; }

#endregion
#endregion
#region Layouts ...

public enum log4netLayoutTypes
{
	[XmlEnum( "log4net.Layout.ExceptionLayout" )]
	ExceptionLayout,
	[XmlEnum( "log4net.Layout.PatternLayout" )]
	PatternLayout,
	[XmlEnum( "log4net.Layout.RawPropertyLayout" )]
	RawPropertyLayout,
	[XmlEnum( "log4net.Layout.RawTimeStampLayout" )]
	RawTimeStampLayout,
	[XmlEnum( "log4net.Layout.RawUtcTimeStampLayout" )]
	RawUtcTimeStampLayout,
	[XmlEnum( "log4net.Layout.SimpleLayout" )]
	SimpleLayout,
	[XmlEnum( "log4net.Layout.XmlLayout" )]
	XmlLayout,
}

/// <summary>
/// Common to all Layout
/// </summary>
public class ignoresException { [XmlAttribute] public bool value; }

/// <summary>
/// Common to all Layout
/// </summary>
public class header { [XmlAttribute] public string value; }

/// <summary>
/// Common to all Layout
/// </summary>
public class footer { [XmlAttribute] public string value; }

/// <summary>
/// Used by the PatternLayout
/// </summary>
public class conversionPattern { [XmlAttribute] public string value; }

/// <summary>
/// Used by the XmlLayout
/// </summary>
public class base64EncodeMessage { [XmlAttribute] public bool value; }

/// <summary>
/// Used by the XmlLayout
/// </summary>
public class base64EncodeProperties { [XmlAttribute] public bool value; }

// Used by the RawPropertyLayout
//public class key - see Shared
#endregion
#region Filters ...

public enum log4netFilterTypes
{
	[XmlEnum( "log4net.Filter.DenyAllFilter" )]
	DenyAllFilter,
	[XmlEnum( "log4net.Filter.LevelMatchFilter" )]
	LevelMatchFilter,
	[XmlEnum( "log4net.Filter.LevelRangeFilter" )]
	LevelRangeFilter,
	[XmlEnum( "log4net.Filter.LoggerMatchFilter" )]
	LoggerMatchFilter,
	[XmlEnum( "log4net.Filter.MdcFilter" )]
	MdcFilter,
	[XmlEnum( "log4net.Filter.NdcFilter" )]
	NdcFilter,
	[XmlEnum( "log4net.Filter.PropertyFilter" )]
	PropertyFilter,
	[XmlEnum( "log4net.Filter.StringMatchFilter" )]
	StringMatchFilter,
}

/// <summary>
/// Used by the LevelMatchFilter, LevelRangeFilter, LoggerMatchFilter, and StringMatchFilter
/// </summary>
public class acceptOnMatch { [XmlAttribute] public bool value; }

/// <summary>
/// Used by the LevelMatchFilter
/// </summary>
public class levelToMatch { [XmlAttribute] public log4netLevel value; }

/// <summary>
/// Used by the LevelRangeFilter
/// </summary>
public class levelMin { [XmlAttribute] public log4netLevel value; }

/// <summary>
/// Used by the LevelRangeFilter
/// </summary>
public class levelMax { [XmlAttribute] public log4netLevel value; }

/// <summary>
/// Used by the LoggerMatchFilter
/// </summary>
public class loggerToMatch { [XmlAttribute] public string value; }

// Used by the PropertyFilter
//public class key - see Shared

/// <summary>
/// Used by the StringMatchFilter
/// </summary>
public class stringToMatch { [XmlAttribute] public string value; }

/// <summary>
/// Used by the StringMatchFilter
/// </summary>
public class regexToMatch { [XmlAttribute] public string value; }

#endregion
#region securityContext

/// <summary>
/// SecurityContext Gets or sets the SecurityContext used for the appender.  
/// </summary>
public class securityContext
{
	[XmlAttribute]
	public securityContextTypes type;

	public userName userName;
	public password password;
	public domain domain;
}

public enum securityContextTypes
{
	[XmlEnum("log4net.Util.NullSecurityContext")]
	NullSecurityContext,
	[XmlEnum("log4net.Util.WindowsSecurityContext")]
	WindowsSecurityContext,
}

public class userName { [XmlAttribute]public string value; }
public class password { [XmlAttribute]public string value; }
public class domain { [XmlAttribute]public string value; }

#endregion
#region AdoNetAppender.parameter

public class parameter
{
	[XmlElement( typeof( dbType ) )]
	[XmlElement( typeof( parameterName ) )]
	[XmlElement( typeof( precision ) )]
	[XmlElement( typeof( scale ) )]
	[XmlElement( typeof( size ) )]
	[XmlElement( typeof( layout ) )]
	public object[] children;
}

/// <summary>
/// DbType Gets or sets the database type for this parameter.  
/// </summary>
public class dbType { [XmlAttribute] public System.Data.DbType value; }

/// <summary>
/// ParameterName Gets or sets the name of this parameter.  
/// </summary>
public class parameterName { [XmlAttribute] public string value; }

/// <summary>
/// Precision Gets or sets the precision for this parameter.  
/// </summary>
public class precision { [XmlAttribute] public byte value; }

/// <summary>
/// Scale Gets or sets the scale for this parameter.  
/// </summary>
public class scale { [XmlAttribute] public byte value; }

/// <summary>
/// Size Gets or sets the size for this parameter.  
/// </summary>
public class size { [XmlAttribute] public int value; }

#endregion
