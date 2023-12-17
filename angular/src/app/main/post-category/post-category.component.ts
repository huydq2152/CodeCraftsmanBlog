import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PostCategoryDto, PostCategoryServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
    templateUrl: './post-category.component.html',
    animations: [appModuleAnimation()],
})
export class PostCategoryComponent extends AppComponentBase implements OnInit {
    postCategories: PostCategoryDto[] = [];
    filter: string = '';

    constructor(injector: Injector, private _postCategoryService: PostCategoryServiceProxy) {
        super(injector);
    }
    ngOnInit(): void {
        throw new Error('Method not implemented.');
        // this.getPostCategory();
    }

    // getPostCategory() {
    //     this._postCategoryService.getAll(this.filter).subscribe((result) => {
    //         this.postCategories = result.items;
    //     });
    // }
}
