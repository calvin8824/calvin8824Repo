function clearErrors() 
{    
    for (var loopCounter = 0; 
        loopCounter < document.forms["contactForm"].elements.length; 
        loopCounter++) 
	{
        if (document.forms["contactForm"].elements[loopCounter]
           .parentElement.className.indexOf("has-") != -1) 
		{
            
            document.forms["contactForm"].elements[loopCounter]
               .parentElement.className = "form-group";
        }
    }    
}

function validateItems() {
    clearErrors();
    var name = document.forms["contactForm"]["name"].value;
    var email = document.forms["contactForm"]["email"].value;
    var phone = document.forms["contactForm"]["phone"].value;
    if (name == "") {
        alert("Must enter a name.");
        document.forms["contactForm"]["name"]
           .parentElement.className = "form-group has-error";
        document.forms["contactForm"]["name"].focus();
        return false;
    }
	if (email == "") {
        alert("Must enter a email.");
        document.forms["contactForm"]["email"]
           .parentElement.className = "form-group has-error";
        document.forms["contactForm"]["email"].focus();
        return false;
    }
	if (phone == "") {
        alert("Must enter a phone number.");
        document.forms["contactForm"]["phone"]
           .parentElement.className = "form-group has-error";
        document.forms["contactForm"]["phone"].focus();
        return false;
    }
	if(!document.getElementById('choice1').checked && 
	!document.getElementById('choice2').checked)
	{
		alert("Must answer: Have you been here before?");
		document.forms["contactForm"]["choice1"].focus();
        return false;
	}
	if(!document.getElementById('mon').checked && 
	!document.getElementById('tues').checked && 
	!document.getElementById('wed').checked && 
	!document.getElementById('thur').checked && 
	!document.getElementById('fri').checked)
	{
		alert("Must choose at least one day.");
		document.forms["contactForm"]["mon"].focus();
        return false;
	}
	
    document.getElementById("results").style.display = "block";
    document.getElementById("submitButton").innerText = "Send Request";
    return false;
}