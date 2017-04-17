/**
 * System configuration for Angular 2
 * Adjust as necessary for your application needs.
 */
(function (global) {
    System.config({
        defaultJSExtensions: true,
        paths: {
            // paths serve as alias
            'npm:': 'node_modules/'
        },
        // map tells the System loader where to look for things
        map: {
            // our app is within the app folder
            app: 'app',
            // angular bundles
            '@angular/core': 'npm:@angular/core/bundles/core.umd.js',
            '@angular/common': 'npm:@angular/common/bundles/common.umd.js',
            '@angular/compiler': 'npm:@angular/compiler/bundles/compiler.umd.js',
            '@angular/platform-browser': 'npm:@angular/platform-browser/bundles/platform-browser.umd.js',
            '@angular/platform-browser-dynamic': 'npm:@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
            '@angular/http': 'npm:@angular/http/bundles/http.umd.js',
            '@angular/router': 'npm:@angular/router/bundles/router.umd.js',
            '@angular/forms': 'npm:@angular/forms/bundles/forms.umd.js',
            // other libraries
            'rxjs': 'npm:rxjs',
            '@commonclient/services': 'npm:@commonclient/services/dist/index.js',
            '@commonclient/components': 'npm:@commonclient/components/dist/index.js',
            '@commonclient/controls': 'npm:@commonclient/controls/dist/index.js',
            '@commonclient/directives': 'npm:@commonclient/directives/dist/index.js',

            'angular2-text-mask': 'npm:angular2-text-mask/dist/angular2TextMask.js',
            'text-mask-core': 'npm:text-mask-core',
            'ng2-bootstrap/ng2-bootstrap': 'npm:ng2-bootstrap/bundles/ng2-bootstrap.umd.js',
            'moment': 'npm:moment/moment.js',
            'moment_ru': 'npm:moment/locale/ru.js',
            'ng-sidebar': 'npm:ng-sidebar',
            'ng2-translate': 'node_modules/ng2-translate/bundles/ng2-translate.umd.js'
        },
        // packages tells the System loader how to load when no filename and/or no extension
        packages: {
            app: {
                main: './office-main.js',
                defaultExtension: 'js'
            },
            rxjs: {
                defaultExtension: 'js'
            },
            'ng2-tooltip': {
                main: 'index.js',
                defaultExtension: 'js'
            },
            mydaterangepicker: {
                defaultExtension: 'js'
            },
            'ng-sidebar': {
                main: 'lib/index',
                defaultExtension: 'js'
            },
            'ng2-translate': {
                defaultExtension: 'js'
            }
        }
    });
})(this);