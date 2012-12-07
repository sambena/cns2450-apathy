package model;

import org.apache.http.HttpResponse;
import model.Communications;

public class Login
{
	private static String password = null;
	private static String name = null;
	private static boolean loggedState = false;
	
	public boolean IsLoggedIn()
	{
		return loggedState;
	}
	
	public static String GetUserName()
	{
		return name;
	}
	
	public static boolean InitialLogin (String _name, String _password)
	{
		HttpResponse response = Communications.PostTransaction(new LogOnModel(_name, password), "Accounts/Login");
		
		//Worked
		if(response.getStatusLine().equals( "200" ))
		{
			loggedState = true;
			password = _password;
			name = _name;
			return true;
		}	
		//Failed
		else
		{
			return false;
		}
	}
}

class LogOnModel
{
    public String UserName;
    public String Password;
    
    LogOnModel(String username, String password)
    {
    	UserName = username;
    	Password = password;
    }

}
