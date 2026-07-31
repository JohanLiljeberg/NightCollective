/**
 * GameFormHandler - Manages dynamic member contributions in game forms
 */
class GameFormHandler {
    constructor(gameId, initialCount = 0) {
        this.gameId = gameId;
        this.currentIndex = initialCount;
        this.container = document.getElementById(`member-contributions-list-${gameId}`);
        this.addButton = document.querySelector(`[data-game-id="${gameId}"].add-contribution-btn`);

        if (!this.container) {
            console.error(`Container not found for game ID: ${gameId}`);
            return;
        }

        this.init();
    }

    init() {
        // Attach event listeners
        if (this.addButton) {
            this.addButton.addEventListener('click', () => this.addContribution());
        }

        // Delegate remove button clicks to container
        this.container.addEventListener('click', (e) => {
            if (e.target.closest('.remove-contribution-btn')) {
                this.removeContribution(e.target.closest('.remove-contribution-btn'));
            }
        });
    }

    /**
     * Add a new member contribution form item
     */
    addContribution() {
        const index = this.currentIndex++;
        const html = this.buildContributionHtml(index);
        this.container.insertAdjacentHTML('beforeend', html);
    }

    /**
     * Remove a contribution item and reindex remaining items
     */
    removeContribution(button) {
        const item = button.closest('.member-contribution-item');
        if (!item) return;

        // Bootstrap fade out animation
        item.classList.add('fade');
        setTimeout(() => {
            item.remove();
            this.reindexItems();
        }, 150);
    }

    /**
     * Reindex all contribution items after removal
     */
    reindexItems() {
        const items = this.container.querySelectorAll('.member-contribution-item');
        items.forEach((item, idx) => {
            const heading = item.querySelector('h6');
            if (heading) {
                heading.textContent = `Member Contribution #${idx + 1}`;
            }
        });
    }

    /**
     * Build HTML for a new contribution item
     */
    buildContributionHtml(index) {
        const memberOptions = this.getMemberOptionsHtml();
        const workAreaCheckboxes = this.getWorkAreaCheckboxesHtml(index);

        return `
            <div class="member-contribution-item card mb-3" data-index="${index}">
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-start mb-3">
                        <h6 class="mb-0">Member Contribution #${index + 1}</h6>
                        <button type="button" class="btn btn-sm btn-danger remove-contribution-btn" data-index="${index}">
                            <span aria-hidden="true">&times;</span> Remove
                        </button>
                    </div>

                    <div class="mb-3">
                        <label class="form-label" for="member-select-${this.gameId}-${index}">Member</label>
                        <select class="form-select" 
                                id="member-select-${this.gameId}-${index}"
                                name="GameForm.MemberContributions[${index}].MemberId" 
                                required>
                            <option value="">Select a member...</option>
                            ${memberOptions}
                        </select>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Involvement Level</label>
                        <div class="btn-group w-100" role="group" aria-label="Involvement level selection">
                            <input type="radio" 
                                   class="btn-check" 
                                   name="GameForm.MemberContributions[${index}].InvolvementLevel" 
                                   id="involvement-${this.gameId}-${index}-0" 
                                   value="0" 
                                   checked 
                                   autocomplete="off" />
                            <label class="btn btn-outline-primary" for="involvement-${this.gameId}-${index}-0">
                                🥉 Supporting
                            </label>

                            <input type="radio" 
                                   class="btn-check" 
                                   name="GameForm.MemberContributions[${index}].InvolvementLevel" 
                                   id="involvement-${this.gameId}-${index}-1" 
                                   value="1" 
                                   autocomplete="off" />
                            <label class="btn btn-outline-primary" for="involvement-${this.gameId}-${index}-1">
                                🥈 Major
                            </label>

                            <input type="radio" 
                                   class="btn-check" 
                                   name="GameForm.MemberContributions[${index}].InvolvementLevel" 
                                   id="involvement-${this.gameId}-${index}-2" 
                                   value="2" 
                                   autocomplete="off" />
                            <label class="btn btn-outline-primary" for="involvement-${this.gameId}-${index}-2">
                                🥇 Lead
                            </label>
                        </div>
                    </div>

                    <div class="mb-0">
                        <label class="form-label">Work Areas <small class="text-muted">(select all that apply)</small></label>
                        <div class="row g-2">
                            ${workAreaCheckboxes}
                        </div>
                    </div>
                </div>
            </div>
        `;
    }

    /**
     * Get member select options from the first existing select element
     */
    getMemberOptionsHtml() {
        const existingSelect = this.container.querySelector('.form-select');
        if (existingSelect) {
            const options = Array.from(existingSelect.options)
                .filter(opt => opt.value !== '')
                .map(opt => `<option value="${opt.value}">${opt.text}</option>`)
                .join('');
            return options;
        }
        return '';
    }

    /**
     * Get work area checkboxes from the first existing item
     */
    getWorkAreaCheckboxesHtml(index) {
        const firstItem = this.container.querySelector('.member-contribution-item');
        if (!firstItem) return '';

        const workAreaContainer = firstItem.querySelector('.row.g-2');
        if (!workAreaContainer) return '';

        // Clone and update the checkboxes for the new index
        const checkboxes = Array.from(workAreaContainer.querySelectorAll('.col-sm-6'));
        return checkboxes.map(col => {
            const checkbox = col.querySelector('input[type="checkbox"]');
            const label = col.querySelector('label');

            if (!checkbox || !label) return '';

            const value = checkbox.value;
            const labelText = label.textContent.trim();
            const newCheckboxId = `work-area-${this.gameId}-${index}-${value}`;

            return `
                <div class="col-sm-6">
                    <div class="form-check">
                        <input class="form-check-input" 
                               type="checkbox" 
                               name="GameForm.MemberContributions[${index}].SelectedWorkAreas" 
                               id="${newCheckboxId}" 
                               value="${value}" />
                        <label class="form-check-label" for="${newCheckboxId}">
                            ${labelText}
                        </label>
                    </div>
                </div>
            `;
        }).join('');
    }
}

// Make it available globally
window.GameFormHandler = GameFormHandler;
