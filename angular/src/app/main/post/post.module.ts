import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { PostRoutingModule } from './post-routing.module';
import { PostComponent } from './post.component';
import { SubheaderModule } from '../../shared/common/sub-header/subheader.module';
import { CreateOrEditPostModalComponent } from './create-or-edit-post-modal.component';
import { InputSwitchModule } from 'primeng/inputswitch';
import { InputTextModule } from 'primeng/inputtext';
import { CheckboxModule } from 'primeng/checkbox';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { EditorModule } from 'primeng/editor';

@NgModule({
    declarations: [PostComponent, CreateOrEditPostModalComponent],
    imports: [
        AppSharedModule,
        PostRoutingModule,
        SubheaderModule,
        InputSwitchModule,
        InputTextModule,
        CheckboxModule,
        RadioButtonModule,
        InputTextareaModule,
        EditorModule,
    ],
})
export class PostModule {}
