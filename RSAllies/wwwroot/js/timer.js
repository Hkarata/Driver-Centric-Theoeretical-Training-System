$(document).ready(function () {
    let timeInMinutes = 30;
    $("#myThing").hide();

    const storedTime = localStorage.getItem("timerRemainingTime");

    // Check if storedTime is null or undefined (falsy value)
    let timeInSeconds;
    if (storedTime) {
        // Parse the stored time if it exists
        timeInSeconds = parseInt(storedTime);
    } else {
        // Set initial time if no stored time exists
        timeInSeconds = 30 * 60;  // 30 minutes in seconds
    }

    // Create a new web worker
    const worker = new Worker('/js/time-worker.js');

    // Send initial time to the worker
    worker.postMessage(timeInSeconds);

    // Listen for messages from the worker
    worker.onmessage = function (event) {
        if (event.data.expired) {
            // Handle timer expiration in the main script
            $("#myThing").show();
            $('.QCard').hide();
            $("#First").css({
                "background-color": "white",
                "box-shadow": "0 0 10px 0 rgba(0,0,0,0.1)",
                "border-radius": "5px",
                "padding": "5px",
                "justify-content": "center"
            });
        } else if (event.data.remainingTime) {
            // save remaining time in local storage
            localStorage.setItem("timerRemainingTime", event.data.remainingTime);

            // Update UI with remaining time (optional)
            let minutes = Math.floor(event.data.remainingTime / 60);
            let seconds = event.data.remainingTime % 60;

            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;

            $("#countdown").html(minutes + ":" + seconds);

            // Update color based on remaining time (optional)
            if (300 < event.data.remainingTime <= 600) {
                $("#Timer").css({
                    "background-color": "#d0e33e"
                });
            }

            if (event.data.remainingTime < 300) {
                $("#Timer").css({
                    "background-color": "red",
                    "color": "white",
                    "font-weight": "600"
                });
            }
        }
    };

    // Rest of your code
});
