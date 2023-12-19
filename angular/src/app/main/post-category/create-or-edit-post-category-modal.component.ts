import { Component, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    CCBServiceProxy,
    CreateOrEditPostCategoryDto,
    PostCategoryDto,
    PostCategoryServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs';

@Component({
    selector: 'createOrEditPostCategoryModal',
    templateUrl: './create-or-edit-post-category-modal.component.html',
})
export class CreateOrEditPostCategoryModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    postCategory: CreateOrEditPostCategoryDto = new CreateOrEditPostCategoryDto();

    filteredPostCategories: PostCategoryDto[];
    selectedPostCategory: PostCategoryDto;

    constructor(
        injector: Injector,
        private _postCategoryService: PostCategoryServiceProxy,
        private _ccbAppSerivce: CCBServiceProxy
    ) {
        super(injector);
    }

    show(postCategory?: CreateOrEditPostCategoryDto): void {
        this.active = true;
        if (postCategory === undefined || postCategory.id === undefined || postCategory.id <= 0) {
            this.postCategory = new CreateOrEditPostCategoryDto();
            this.modal.show();
        } else {
            this._postCategoryService.getPostCategoryForEdit(postCategory.id).subscribe((postCategoryResult) => {
                this.postCategory = postCategoryResult.postCategory;
                this.modal.show();
            });
        }
    }

    onShown(): void {
        document.getElementById('Code').focus();
    }

    save(): void {
        let input = this.postCategory;
        if (this.selectedPostCategory != undefined) {
            input.parentId = this.selectedPostCategory.id;
        }

        this.saving = true;
        this._postCategoryService
            .createOrEdit(input)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    filterPostCategories(event): void {
        this._ccbAppSerivce.getPostCategories(event.query, undefined, undefined).subscribe((postCategories) => {
            this.filteredPostCategories = postCategories;
        });
    }

    public selectedItemDisplay(postCategory: PostCategoryDto): string {
        return postCategory.code + ' ' + postCategory.name;
    }
}
