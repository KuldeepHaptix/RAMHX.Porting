 $(document).ready(function(){
		$("#email").click(function(){
			$("#email").removeClass("error_input");
		}); 

		$("#message").click(function(){
			$("#message").removeClass("error_input");
		});
		
        $('#send').click(function(e){
            
            //Stop form submission & check the validation
            e.preventDefault();
            
            // Variable declaration
            var error = false;
            var name = $('#name').val();
            var email = $('#email').val();
            var guest = $('#guest').val();
			var checkin = $('#checkin').val();
			var checkout = $('#checkout').val();
			var room = $('#room').val();
			var message = $('#message').val();
            
         	// Form field validation
            
			if(name.length == 0){
                var error = true;
				$("#name").addClass("error_input");
			}else{
				$("#name").removeClass("error_input");
			}
			
            if(email.length == 0 || email.indexOf('@') == '-1'){
                var error = true;
				$("#email").addClass("error_input");
			}else{
				$("#email").removeClass("error_input");
			}
			
			if($('#guest option:selected').index() == 0){
                var error = true;
				$("#guest").addClass("error_input");
			}else{
				$("#guest").removeClass("error_input");
			}
			
			if(checkin.length == 0){
                var error = true;
				$("#checkin").addClass("error_input");
			}else{
				$("#checkin").removeClass("error_input");
			}
			
			if(checkout.length == 0){
                var error = true;
				$("#checkout").addClass("error_input");
			}else{
				$("#checkout").removeClass("error_input");
			}
			
			if($('#room option:selected').index() == 0){
                var error = true;
				$("#room").addClass("error_input");
			}else{
				$("#room").removeClass("error_input");
			}
			
            
            // If there is no validation error, next to process the mail function
            if(error == false){
               // Disable submit button just after the form processed 1st time successfully.
                $('#send').attr({'disabled' : 'true', 'value' : 'Sending...' });
                
				/* Post Ajax function of jQuery to get all the data from the submission of the form as soon as the form sends the values to email.php*/
                $.post("reservation.php", $("#booking_form").serialize(),function(result){
                    //Check the result set from email.php file.
                    if(result == 'sent'){
                        //If the email is sent successfully, remove the submit button
                         $('#submit').remove();
                        //Display the success message
                        $('#mail_success').fadeIn(500);
                    }else{
                        //Display the error message
                        $('#mail_failed').fadeIn(500);
                        // Enable the submit button again
                        $('#send').removeAttr('disabled').attr('value', 'Send The Message');
                    }
                });
            }
        });    
    });