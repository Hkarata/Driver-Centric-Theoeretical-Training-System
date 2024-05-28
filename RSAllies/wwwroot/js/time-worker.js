let timeInSeconds;
let countdownInterval;

self.onmessage = function (event) {
    // Receive the initial time from the main script
    timeInSeconds = event.data;

    countdownInterval = setInterval(function () {
        // Update timer logic
        let minutes = Math.floor(timeInSeconds / 60);
        let seconds = timeInSeconds % 60;

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        // Check for timer expiration and handle it if needed
        if (timeInSeconds <= 0) {
            // Send message to main script about timer expiration
            self.postMessage({ expired: true });
            clearInterval(countdownInterval);
        }

        timeInSeconds--;

        self.postMessage({ remainingTime: timeInSeconds });

    }, 1000);
};
