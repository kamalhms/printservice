
var ws;
$(document).ready(function(){
	connectSocket();
	$('#inputFile').on('change',function(){
		
    var reader = new FileReader();
   reader.readAsDataURL(this.files[0]);
   reader.onload = function () {
     sendPDF(reader.result);
   };
   reader.onerror = function (error) {
     console.log('Error: ', error);
   };
	//console.log(data);
	//sendPDF(data);
	})
})
function connectSocket(){
    ws = new WebSocket("ws://localhost:6543/print");
    ws.onopen = function() {
        LogMessage("Connection Established")
    };

    ws.onmessage = function(evt) {
		var lst = evt.data.split('___');
		if(lst[0]=='printerlist')
		{
			var html;
		$.each(lst,function(index,item)
		{
			if(index>0){
			html+= '<option value="'+item+'">' + item + '</option>'}
		})
		$('#printerlist').html(html);
        //LogMessage(evt.data);
		}
		else{
			LogMessage(evt.data);
		}
    };

    ws.onclose = function() {
        LogMessage("Connection is closed...");
    };	
}

function GetPrinters() {
	if(ws.readyState!=1)
	{
		connectSocket();
	}
    if (ws) {
        var content = {
			RequestType : "PrinterList"
		};
        ws.send(JSON.stringify(content));
    }
}

function PrintInvoice(billNo,patientId){
	if(ws.readyState!=1)
	{
		connectSocket();
	}
    if (ws) {
        var content = {
			RequestType : "PrintInvoice",
			ConnectionString : "get from local stroage",
			PrinterName : $('#printerlist').val(),
			PrintingType : "Invoice",
			BillNo : billNo,
			PatientId : patientId
		};
        ws.send(JSON.stringify(content));
    }
}

function LogMessage(message)
{
	$('body').append('<p>' + message + '</p>');
}

function sendPDF(base64Str)
{
	if(ws.readyState!=1)
	{
		connectSocket();
	}
    if (ws) {
        var content = {
			RequestType : "PDF",
			ConnectionString : "get from local stroage",
			PrinterName : $('#printerlist').val(),
			PrintingType : "Invoice",
			pdfContent : base64Str
		};
		//console.log(content);
		//console.log(JSON.stringify(content));
        ws.send(JSON.stringify(content));
    }
}


