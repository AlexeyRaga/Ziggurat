(function ($) {

    $.fn.extend({
        doAlert: function (e) {
            return this.each(function () {
                var target = $(this);
                target.addClass('disabled');
            });
        },

        pullRequestWithProgress: function (url, options) {
            var defaults = {
                step: 10,
                predicate: function (data) { return data == true; },
                onFinished: function (data) { },
                onNotFinished: function() { },
                onError: function (data) { }
            };

            var options = $.extend(defaults, options);

            var element = $(this[0]);
            var bar = $('div.bar', element);

            var done = 0;
            setProgress(done);

            var doCall = function () { makeRequest(url, options); }
            var scheduleAnotherCall = function () { setTimeout(doCall, 1000); }

            doCall();

            function makeRequest(url, options) {
                done = done + options.step;
                if (done > 100) done = 100;
                setProgress(done);

                if (done == 100) {
                    bar.addClass('bar-danger');
                    options.onNotFinished();
                    return;
                }

                $.get(url)
                .success(function (data) {
                    var result = options.predicate(data);
                    if (result) {
                        setProgress(100);
                        bar.addClass('bar-success');
                        options.onFinished(data);
                    } else {
                        scheduleAnotherCall();
                    }
                })
                .error(function (data) {
                    options.onError(data);
                    scheduleAnotherCall();
                });
            }

            function setProgress(progress) {
                bar.css('width', progress + '%');
            }

            return this;
        }
    });
    

})(jQuery);