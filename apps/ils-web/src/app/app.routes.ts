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
      import('@kathanika/ils-features').then((x) => x.HomeModule),
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('@kathanika/ils-features').then((x) => x.AuthModule),
  },
  {
    path: 'publications',
    loadChildren: () =>
      import('@kathanika/ils-features').then((x) => x.PublicationsModule),
  },
  {
    path: 'members',
    loadChildren: () =>
      import('@kathanika/ils-features').then((x) => x.MembersModule),
  },
];
