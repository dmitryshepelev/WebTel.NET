//import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AuthAppModule } from './app.module';

// Extend Observable through the app
import 'rxjs/Rx';

//enableProdMode();

platformBrowserDynamic().bootstrapModule(AuthAppModule);