package com.example.apathybudget;


import android.os.Bundle;
import android.app.Activity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;
import android.support.v4.app.NavUtils;

public class LogInActivity extends Activity {
	
	EditText etEmailAddress;
	EditText etPassword;
	Button btnSubmit;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_log_in);
        
		etEmailAddress = (EditText) this.findViewById(R.id.etEmailAddress);
		etPassword = (EditText) this.findViewById(R.id.etPassword);
		btnSubmit = (Button) this.findViewById( R.id.btnSubmit );
		
		btnSubmit.setOnClickListener( new View.OnClickListener()
		{
			public void onClick(View v)
			{
				String emailAddress = etEmailAddress.toString();
				String password = etPassword.toString();
//				Toast.makeText( this, resId, duration )
			}
		} );

    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.activity_log_in, menu);
        return true;
    }

    
}
