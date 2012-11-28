package com.example.apathybudget;

import java.text.DateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

import org.apache.http.HttpResponse;

import model.Communications;
import model.Transaction;

import android.os.Bundle;
import android.app.Activity;
import android.app.DatePickerDialog;
import android.view.Menu;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ScrollView;
import android.widget.Spinner;
import android.widget.Toast;

public class TransactionActivity extends Activity
{
	public static Spinner spnrEnvelope;
	public static Spinner spnrType;
	public static Button btnCreate;
	public static DatePicker dpDate;
	public static EditText etAmount;
	public static EditText etPayee;
	public static EditText etNotes;
	public static DatePicker myDatePicker;
	
	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate( savedInstanceState );
		setContentView( R.layout.activity_transaction );
		
		spnrEnvelope = (Spinner) findViewById( R.id.spnrEnvelope );
		spnrType = (Spinner) findViewById( R.id.spnrType );
		etAmount = (EditText) findViewById( R.id.etAmount);
		etPayee = (EditText) findViewById( R.id.etPayee);
		etNotes = (EditText) findViewById( R.id.etNotes);
		btnCreate = (Button) findViewById( R.id.btnCreate);
		myDatePicker = (DatePicker) findViewById(R.id.mydatepicker);
		
		//SetTypeSpinner( );
		ArrayList<String> arrayList = new ArrayList<String>();
		arrayList.add( "Expense" );
		arrayList.add( "Deposit" );
		SetTypeSpinner( arrayList );
		
		//SetEnvelopesSpinner
		SetEnvelopesSpinner(GetEnvelopesFromServer());
		
		btnCreate.setOnClickListener( new View.OnClickListener()
		{
			
			public void onClick(View v)
			{
				SendTransaction();
			}
		});		
	}

	public void MakeToast(String message)
	{
			Toast.makeText( this, message, Toast.LENGTH_SHORT).show();
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu)
	{
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate( R.menu.activity_transaction, menu );
		return true;
	}
	
	public String[] GetEnvelopesFromServer()
	{
		String [] tempEnvelopeArray = {"cars", "household", "other"};
		//HttpResponse response = Communications.PostTransaction( transaction, "controller" );
		return tempEnvelopeArray;
	}
	
	public void SetEnvelopesSpinner(String [] arrayList)
	{
		ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, arrayList);
		adapter.setDropDownViewResource( android.R.layout.simple_spinner_dropdown_item );
		spnrEnvelope.setAdapter(adapter);
	}
	
	public void SetTypeSpinner(ArrayList<String> arrayList)
	{
		ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, arrayList);
		adapter.setDropDownViewResource( android.R.layout.simple_spinner_dropdown_item );
		spnrType.setAdapter(adapter);
	}
	
	public String GetEnvelope()
	{
		return spnrEnvelope.getSelectedItem().toString();
	}
	
	public String GetType()
	{
		return spnrType.getSelectedItem().toString();
	}
	
	public String GetAmount()
	{
		return etAmount.getText().toString();
	}
	
	public String GetDate()
	{
		return DateFormat.getDateInstance().format(myDatePicker.getCalendarView().getDate());
	}
	
	public String GetPayee()
	{
		return etPayee.getText().toString();
	}
	
	public String GetNotes()
	{
		return etNotes.getText().toString();
	}
	
	public Transaction GetTransaction()
	{
		Transaction t = new Transaction();
		t.envelope = GetEnvelope();
		t.type = GetType();
		t.amount = GetAmount();
		t.date = GetDate();
		t.payee = GetPayee();
		t.notes = GetNotes();
		
		return t;
	}
	
	public void SendTransaction()
	{
		MakeToast( "Send Transaction was called");
		Transaction transaction = GetTransaction();
		HttpResponse response = Communications.PostTransaction( transaction, "controller" );

		//Worked
//		if(response.)
		{
			MakeToast("Transaction Accepted!");
		}
		//Failed
//		if(response.get)
		{
			MakeToast( "Communications failed, try again." );
		}
	}
}
