package model;

public class Login
{
	private String password = null;
	private String userName = null;
	private boolean loggedState = false;
	
	public boolean IsLoggedIn()
	{
		return loggedState;
	}
}
