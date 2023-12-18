import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { PostCategoryRoutingModule } from './post-category-routing.module';
import { PostCategoryComponent } from './post-category.component';
import { SubheaderModule } from '../../shared/common/sub-header/subheader.module';
import { CreateOrEditPostCategoryModalComponent } from './create-or-edit-post-category-modal.component';
import { InputSwitchModule } from 'primeng/inputswitch';

@NgModule({
    declarations: [PostCategoryComponent, CreateOrEditPostCategoryModalComponent],
    imports: [AppSharedModule, PostCategoryRoutingModule, SubheaderModule, InputSwitchModule],
})
export class PostCategoryModule {}
