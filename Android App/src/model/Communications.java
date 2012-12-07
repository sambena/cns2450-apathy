package model;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.URI;
import java.net.URISyntaxException;

import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.client.utils.URIBuilder;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;

import android.renderscript.ProgramFragmentFixedFunction.Builder;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

public class Communications
{
	public static String userName = Login.GetUserName();
	public static String domain = "apathy.mptolman.net";
	public static String host = domain + "/api";
	public boolean isAuthenticated = false;
	public static URIBuilder builder;
	public static URI uri = null;
	public static HttpResponse response = null;
	
	public static HttpResponse PostTransaction(Object object, String controller)
	{
		//Create the uri
		builder = new URIBuilder();
		builder.setScheme("http://").setHost( host )
		.setPath("/" + userName)
		.setPath( "/" + controller );
		try
		{
			uri = builder.build();
		} catch ( URISyntaxException e1 )
		{
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
		
		
		DefaultHttpClient client = new DefaultHttpClient();
		HttpPost post = new HttpPost( uri );
		
		
		try
		{
			Gson gson = new GsonBuilder().create();
			String test = gson.toJson( object );
			
			StringEntity input = new StringEntity( test );
			input.setContentType( "application/json; charset=utf-8" );
			post.setEntity( input );
			response = client.execute( post );
			BufferedReader rd = new BufferedReader( new InputStreamReader(
					response.getEntity().getContent() ) );
			
			String line = "";
			if ( ( line = rd.readLine() ) != null )
			{
				line = rd.readLine();
			}
		}
		
		catch ( IOException e )
		{
			e.printStackTrace();
		}
		
		return response;
	}
}
