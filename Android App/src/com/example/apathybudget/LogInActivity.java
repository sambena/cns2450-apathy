package com.example.apathybudget;


import model.Login;
import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;
import android.support.v4.app.NavUtils;

public class LogInActivity extends Activity {
	
	EditText etName;
	EditText etPassword;
	Button btnSubmit;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_log_in);
        
		etName = (EditText) this.findViewById(R.id.etName);
		etPassword = (EditText) this.findViewById(R.id.etPassword);
		btnSubmit = (Button) this.findViewById( R.id.btnSubmit );
		
		btnSubmit.setOnClickListener( new View.OnClickListener()
		{
			public void onClick(View v)
			{
				String name = etName.toString();
				String password = etPassword.toString();
			
				if(Login.InitialLogin( name, password))
				{
					Toast.makeText( LogInActivity.this, "Login Successful", 3000 );
					//Goto Page Transactions
					Intent intent = new Intent(LogInActivity.this, TransactionActivity.class);
					startActivity(intent);
				}
				else
				{
					Toast.makeText( LogInActivity.this, "Login failed", 3000 );
				}
			}
		} );

    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.activity_log_in, menu);
        return true;
    }

    
}
