import { platformBrowser } from '@angular/platform-browser';
import { AppModule } from './app/app-module';
import { bootstrapApplication } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { App } from './app/app';

bootstrapApplication(App, {
  providers: [
    provideAnimations(),
  ],
}).catch(err => console.error(err));

platformBrowser().bootstrapModule(AppModule, {
  ngZoneEventCoalescing: true,
})
  .catch(err => console.error(err));
