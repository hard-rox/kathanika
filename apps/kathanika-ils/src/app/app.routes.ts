import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    loadChildren: () =>
      import('@kathanika/ils')
        .then((x) => x.HomeModule),
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('@kathanika/ils')
        .then(x => x.AuthModule),
  },
  {
    path: 'authors',
    loadChildren: () =>
      import('@kathanika/ils').then((x) => x.AuthorsModule),
  },
  {
    path: 'publications',
    loadChildren: () =>
      import('@kathanika/ils').then(
        (x) => x.PublicationsModule
      ),
  },
  {
    path: 'publishers',
    loadChildren: () =>
      import('@kathanika/ils').then(
        (x) => x.PublishersModule
      ),
  },
];
