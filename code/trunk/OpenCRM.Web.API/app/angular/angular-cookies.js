
(function (window, angular, undefined) {
    'use strict';


    /**
     * @ngdoc module
     * @name ngCookies
     * @description
     *
     * # ngCookies
     *
     * The `ngCookies` module provides a convenient wrapper for reading and writing browser cookies.
     *
     *
     * <div doc-module-components="ngCookies"></div>
     *
     * See {@link ngCookies.$cookies `$cookies`} for usage.
     */
    function $$CookieWriter($document, $log, $browser) {
        var cookiePath = $browser.baseHref();
        var rawDocument = $document[0];

        function buildCookieString(name, value, options) {
            var path, expires;
            options = options || {};
            expires = options.expires;
            path = angular.isDefined(options.path) ? options.path : cookiePath;
            if (angular.isUndefined(value)) {
                expires = 'Thu, 01 Jan 1970 00:00:00 GMT';
                value = '';
            }
            if (angular.isString(expires)) {
                expires = new Date(expires);
            }

            var str = encodeURIComponent(name) + '=' + encodeURIComponent(value);
            str += path ? ';path=' + path : '';
            str += options.domain ? ';domain=' + options.domain : '';
            str += expires ? ';expires=' + expires.toUTCString() : '';
            str += options.secure ? ';secure' : '';

            // per http://www.ietf.org/rfc/rfc2109.txt browser must allow at minimum:
            // - 300 cookies
            // - 20 cookies per unique domain
            // - 4096 bytes per cookie
            var cookieLength = str.length + 1;
            if (cookieLength > 4096) {
                $log.warn("Cookie '" + name +
                  "' possibly not set or overflowed because it was too large (" +
                  cookieLength + " > 4096 bytes)!");
            }

            return str;
        }

        return function (name, value, options) {
            rawDocument.cookie = buildCookieString(name, value, options);
        };
    }

    function $$CookieReader($document) {
        var rawDocument = $document[0] || {};
        var lastCookies = {};
        var lastCookieString = '';

        function safeDecodeURIComponent(str) {
            try {
                return decodeURIComponent(str);
            } catch (e) {
                return str;
            }
        }

        return function () {
            var cookieArray, cookie, i, index, name;
            var currentCookieString = rawDocument.cookie || '';

            if (currentCookieString !== lastCookieString) {
                lastCookieString = currentCookieString;
                cookieArray = lastCookieString.split('; ');
                lastCookies = {};

                for (i = 0; i < cookieArray.length; i++) {
                    cookie = cookieArray[i];
                    index = cookie.indexOf('=');
                    if (index > 0) { //ignore nameless cookies
                        name = safeDecodeURIComponent(cookie.substring(0, index));
                        // the first value that is seen for a cookie is the most
                        // specific one.  values for the same cookie name that
                        // follow are for less specific paths.
                        if (angular.isUndefined(lastCookies[name])) {
                            lastCookies[name] = safeDecodeURIComponent(cookie.substring(index + 1));
                        }
                    }
                }
            }
            return lastCookies;
        };
    }

    $$CookieReader.$inject = ['$document'];

    $$CookieWriter.$inject = ['$document', '$log', '$browser'];

    angular.module('ngCookies', ['ng'])

    .provider('$$cookieWriter', function $$CookieWriterProvider() {
        this.$get = $$CookieWriter;
    })
    .provider('$$cookieReader', function $$CookieReaderProvider() {
        this.$get = $$CookieReader;
    })
    /**
     * @ngdoc provider
     * @name $cookiesProvider
     * @description
     * Use `$cookiesProvider` to change the default behavior of the {@link ngCookies.$cookies $cookies} service.
     * */
    .provider('$cookies', [function $CookiesProvider() {
        /**
         * @ngdoc property
         * @name $cookiesProvider#defaults
         * @description
         *
         * Object containing default options to pass when setting cookies.
         *
         * The object may have following properties:
         *
         * - **path** - `{string}` - The cookie will be available only for this path and its
         *   sub-paths. By default, this would be the URL that appears in your base tag.
         * - **domain** - `{string}` - The cookie will be available only for this domain and
         *   its sub-domains. For obvious security reasons the user agent will not accept the
         *   cookie if the current domain is not a sub domain or equals to the requested domain.
         * - **expires** - `{string|Date}` - String of the form "Wdy, DD Mon YYYY HH:MM:SS GMT"
         *   or a Date object indicating the exact date/time this cookie will expire.
         * - **secure** - `{boolean}` - The cookie will be available only in secured connection.
         *
         * Note: by default the address that appears in your `<base>` tag will be used as path.
         * This is important so that cookies will be visible for all routes in case html5mode is enabled
         *
         **/
        var defaults = this.defaults = {};

        function calcOptions(options) {
            return options ? angular.extend({}, defaults, options) : defaults;
        }

        /**
         * @ngdoc service
         * @name $cookies
         *
         * @description
         * Provides read/write access to browser's cookies.
         *
         * <div class="alert alert-info">
         * Up until Angular 1.3, `$cookies` exposed properties that represented the
         * current browser cookie values. In version 1.4, this behavior has changed, and
         * `$cookies` now provides a standard api of getters, setters etc.
         * </div>
         *
         * Requires the {@link ngCookies `ngCookies`} module to be installed.
         *
         * @example
         *
         * ```js
         * angular.module('cookiesExample', ['ngCookies'])
         *   .controller('ExampleController', ['$cookies', function($cookies) {
         *     // Retrieving a cookie
         *     var favoriteCookie = $cookies.get('myFavorite');
         *     // Setting a cookie
         *     $cookies.put('myFavorite', 'oatmeal');
         *   }]);
         * ```
         */
        this.$get = ['$$cookieReader', '$$cookieWriter', function ($$cookieReader, $$cookieWriter) {
            return {
                /**
                 * @ngdoc method
                 * @name $cookies#get
                 *
                 * @description
                 * Returns the value of given cookie key
                 *
                 * @param {string} key Id to use for lookup.
                 * @returns {string} Raw cookie value.
                 */
                get: function (key) {
                    return $$cookieReader()[key];
                },

                /**
                 * @ngdoc method
                 * @name $cookies#getObject
                 *
                 * @description
                 * Returns the deserialized value of given cookie key
                 *
                 * @param {string} key Id to use for lookup.
                 * @returns {Object} Deserialized cookie value.
                 */
                getObject: function (key) {
                    var value = this.get(key);
                    return value ? angular.fromJson(value) : value;
                },

                /**
                 * @ngdoc method
                 * @name $cookies#getAll
                 *
                 * @description
                 * Returns a key value object with all the cookies
                 *
                 * @returns {Object} All cookies
                 */
                getAll: function () {
                    return $$cookieReader();
                },

                /**
                 * @ngdoc method
                 * @name $cookies#put
                 *
                 * @description
                 * Sets a value for given cookie key
                 *
                 * @param {string} key Id for the `value`.
                 * @param {string} value Raw value to be stored.
                 * @param {Object=} options Options object.
                 *    See {@link ngCookies.$cookiesProvider#defaults $cookiesProvider.defaults}
                 */
                put: function (key, value, options) {
                    $$cookieWriter(key, value, calcOptions(options));
                },

                /**
                 * @ngdoc method
                 * @name $cookies#putObject
                 *
                 * @description
                 * Serializes and sets a value for given cookie key
                 *
                 * @param {string} key Id for the `value`.
                 * @param {Object} value Value to be stored.
                 * @param {Object=} options Options object.
                 *    See {@link ngCookies.$cookiesProvider#defaults $cookiesProvider.defaults}
                 */
                putObject: function (key, value, options) {
                    this.put(key, angular.toJson(value), options);
                },

                /**
                 * @ngdoc method
                 * @name $cookies#remove
                 *
                 * @description
                 * Remove given cookie
                 *
                 * @param {string} key Id of the key-value pair to delete.
                 * @param {Object=} options Options object.
                 *    See {@link ngCookies.$cookiesProvider#defaults $cookiesProvider.defaults}
                 */
                remove: function (key, options) {
                    $$cookieWriter(key, undefined, calcOptions(options));
                }
            };
        }];
    }])

    .factory('$cookieStore', ['$cookies', function ($cookies) {

        return {
            /**
             * @ngdoc method
             * @name $cookieStore#get
             *
             * @description
             * Returns the value of given cookie key
             *
             * @param {string} key Id to use for lookup.
             * @returns {Object} Deserialized cookie value, undefined if the cookie does not exist.
             */
            get: function (key) {
                return $cookies.getObject(key);
            },

            getObject: function (key) {
                return $cookies.getObject(key);
            },

            /**
             * @ngdoc method
             * @name $cookieStore#put
             *
             * @description
             * Sets a value for given cookie key
             *
             * @param {string} key Id for the `value`.
             * @param {Object} value Value to be stored.
             */
            put: function (key, value) {
                $cookies.putObject(key, value);
            },

            putObject: function (key, value, options) {
                $cookies.putObject(key, value, options);
            },

            /**
             * @ngdoc method
             * @name $cookieStore#remove
             *
             * @description
             * Remove given cookie
             *
             * @param {string} key Id of the key-value pair to delete.
             */
            remove: function (key) {
                $cookies.remove(key);
            }
        };

    }]);

})(window, window.angular);