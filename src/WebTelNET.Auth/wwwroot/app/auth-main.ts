//import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AuthAppModule } from './auth-app.module';

// Extend Observable through the app
import 'rxjs/Rx';

//enableProdMode();

platformBrowserDynamic().bootstrapModule(AuthAppModule);