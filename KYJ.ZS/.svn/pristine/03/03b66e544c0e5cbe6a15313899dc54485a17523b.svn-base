using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;


/// <summary>
/// Handler 的摘要说明
/// </summary>
public abstract class Handler
{
	public Handler(HttpContext context)
	{
        this.Request = context.Request;
        this.Response = context.Response;
        this.Context = context;
        this.Server = context.Server;
	}

    public abstract void Process();

    protected void WriteJson(object response)
    {
        string jsonpCallback = Request["callback"],
            json = JsonConvert.SerializeObject(response);
        if (String.IsNullOrWhiteSpace(jsonpCallback))
        {
            Response.AddHeader("Content-Type", "text/plain");
            Response.Write(json);
        }
        else 
        {
            Response.AddHeader("Content-Type", "application/javascript");
            Response.Write(String.Format("{0}({1});", jsonpCallback, json));
        }
        //Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:13745");
        Response.AddHeader("Access-Control-Allow-Origin","*");
        Response.AddHeader("Access-Control-Allow-Headers","X-Requested-With,X_Requested_With");
        Response.End();
    }

    public HttpRequest Request { get; private set; }
    public HttpResponse Response { get; private set; }
    public HttpContext Context { get; private set; }
    public HttpServerUtility Server { get; private set; }
}