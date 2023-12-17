import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { PostCategoryRoutingModule } from './post-category-routing.module';
import { PostCategoryComponent } from './post-category.component';
import { SubheaderModule } from '../../shared/common/sub-header/subheader.module';

@NgModule({
    declarations: [PostCategoryComponent],
    imports: [AppSharedModule, PostCategoryRoutingModule, SubheaderModule],
})
export class PostCategoryModule {}
