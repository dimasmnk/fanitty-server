import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './pages/profile/profile.component';
import { ProfileEditorComponent } from './pages/profile-editor/profile-editor.component';

const routes: Routes = [
  { path: 'profile/:username', component: ProfileComponent },
  { path: 'profile-editor', component: ProfileEditorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
