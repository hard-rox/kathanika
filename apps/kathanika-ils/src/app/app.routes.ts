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
  // {
  //   path: 'authors',
  //   loadChildren: () =>
  //     import('./features/authors/authors.module').then((x) => x.AuthorsModule),
  // },
  // {
  //   path: 'publications',
  //   loadChildren: () =>
  //     import('./features/publications/publications.module').then(
  //       (x) => x.PublicationsModule
  //     ),
  // },
  // {
  //   path: 'publishers',
  //   loadChildren: () =>
  //     import('./features/publishers/publishers.module').then(
  //       (x) => x.PublishersModule
  //     ),
  // },
];
